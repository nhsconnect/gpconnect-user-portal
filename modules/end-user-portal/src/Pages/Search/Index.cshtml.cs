using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public partial class SearchModel : BaseModel
{
  private readonly IRequestService _requestService;

  public SearchModel(IOptions<ApplicationParameters> applicationParameters, IRequestService requestService) : base(applicationParameters)
  {
    _requestService = requestService;
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
      var searchResults = await _requestService.ExecuteApiGetAsync<SearchRequest, List<SearchResultEntry>>(CreateSearchRequest(), "transparency-site");
      SearchResult = new SearchResult() { SearchResults = searchResults };
    }
    catch
    {
      throw;
    }
  }

  private SearchRequest CreateSearchRequest() => new SearchRequest()
  {
    SiteOdsCode = ProviderOdsCode,
    SiteName = ProviderName
  };

  private void ClearModelState()
  {
    ModelState.ClearValidationState("ProviderOdsCode");
  }
}
