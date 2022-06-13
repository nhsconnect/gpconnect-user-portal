using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public partial class SearchModel : BaseModel
{
  private readonly ISiteService _siteService;

  public SearchModel(IOptions<ApplicationParameters> applicationParameters, ISiteService siteService) : base(applicationParameters)
  {
    _siteService = siteService;
  }

  public IActionResult OnGet()
  {
    ClearModelState();
    return Page();
  }

  public async Task<IActionResult> OnPostSearchAsync()
  {
    DisplaySearchInvalid = false;    

    if (HasMultipleSearchParameters)
    {
      DisplaySearchInvalid = true;
      return Page();
    }

    if (!ModelState.IsValid || !IsValidSearch)
    {
      DisplaySearchInvalid = !IsValidSearch;
      return Page();
    }

    var results = await GetSearchResults();

    if (results.Count == 0)
    {
      return RedirectToPage("/Search/NoResults", new Dictionary<string,object> {
        { "query", ProviderName },
        { "mode", SearchMode.Name }
      });
    }

    SearchResult = new SearchResult { SearchResults = results };
    return Page();
  }

  public IActionResult OnPostClear()
  {
    ProviderOdsCode = null;
    ProviderName = null;
    ModelState.Clear();
    return Page();
  }

  private Task<List<SearchResultEntry>> GetSearchResults()
  {
    try
    {
      return _siteService.SearchSitesAsync(new SearchRequest()
      {
        SiteOdsCode = ProviderOdsCode,
        SiteName = ProviderName
      });
      
    }
    catch
    {
      throw;
    }
  }

  private void ClearModelState()
  {
    ModelState.ClearValidationState("ProviderOdsCode");
  }
}
