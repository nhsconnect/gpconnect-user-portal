using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public partial class SearchModel : BaseModel
{
  private readonly ILogger<SearchModel> _logger;
  private readonly IRequestService _requestService;

  public SearchModel(ILogger<SearchModel> logger, IOptions<ApplicationParameters> applicationParameters, IRequestService requestService) : base(applicationParameters)
  {
    _logger = logger;
    _requestService = requestService;
  }

  public async Task<IActionResult> OnGet()
  {
    ClearModelState();
    await PopulateControls();
    return Page();
  }

  private async Task PopulateControls()
  {
    CcgList = await GetCCGs();
  }

  public async Task<IActionResult> OnPostSearchAsync()
  {
    if (ModelState.IsValid && IsValidSearch && !HasMultipleSearchParameters)
    {
      DisplaySearchInvalid = false;
      await GetSearchResults();
    }
    else
    {
      DisplaySearchInvalid = !IsValidSearch || HasMultipleSearchParameters;
    }
    await PopulateControls();
    return Page();
  }

  public IActionResult OnPostClear()
  {
    ProviderOdsCode = null;
    SelectedCcgIcbOdsCode = null;
    SelectedCcgIcbName = null;
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
    SiteName = ProviderName,
    CcgIcbOdsCode = SelectedCcgIcbOdsCode,
    CcgIcbName = SelectedCcgIcbName
  };

  private void ClearModelState()
  {
    ModelState.ClearValidationState("ProviderOdsCode");
  }

  private async Task<List<CcgModel>> GetCCGs()
  {
    var ccgList = await _requestService.ExecuteApiGetAsync<List<CcgModel>>("ccg");
    return ccgList;
  }
}
