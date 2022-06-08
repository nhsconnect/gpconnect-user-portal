using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
    if (HasMultipleSearchParameters)
    {
      DisplaySearchInvalid = true;
    }
    else
    {
      if (ModelState.IsValid && IsValidSearch)
      {
        DisplaySearchInvalid = false;
        await GetSearchResults();
      }
      else
      {
        DisplaySearchInvalid = !IsValidSearch;
      }
    }
    return Page();
  }

  public IActionResult OnPostClear()
  {
    ProviderOdsCode = null;
    ProviderName = null;
    ModelState.Clear();
    return Page();
  }

  private async Task GetSearchResults()
  {
    try
    {
      var searchResults = await _siteService.SearchSitesAsync<SearchRequest, List<SearchResultEntry>>(new SearchRequest()
      {
        SiteOdsCode = ProviderOdsCode,
        SiteName = ProviderName
      });
      SearchResult = new SearchResult() { SearchResults = searchResults };
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
