using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
    private readonly ISiteService _siteService;

    public ResultsModel(IOptions<ApplicationParameters> applicationParameters, ISiteService siteService) : base(applicationParameters)
    {
        _siteService = siteService;
    }

    public async Task<IActionResult> OnGet(string query, SearchMode mode)
    {
        try
        {
            var searchResults = await _siteService.SearchSitesAsync(new SearchRequest()
            {
                Query = query,
                Mode = mode
            });

            if (searchResults.Count == 0)
            {
                return RedirectToPage("./NoResults", new { query = query, mode = mode });
            }

            SearchResult = new SearchResult() { SearchResults = searchResults };
        }
        catch
        {
            throw;
        }
        return Page();
    }
}
