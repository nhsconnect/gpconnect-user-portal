using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
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

    public async Task<IActionResult> OnGet()
    {
        if (!ModelState.IsValid) 
        {
            return RedirectToPage("./Name");
        }

        try
        {
            var searchResults = await _siteService.SearchSitesAsync(Query, Mode);

            if (searchResults.Count == 0)
            {
                return RedirectToPage("./NoResults", new { query = Query, mode = Mode });
            }

            if (searchResults.Count == 1)
            {
                return RedirectToPage("./Detail", new { 
                    id = searchResults[0].SiteDefinitionId, 
                    query = Query, 
                    mode = Mode 
                });
            }

            searchResults.Sort((x, y) => x.SiteName.CompareTo(y.SiteName));

            SearchResult = new SearchResult() { SearchResults = searchResults };
        }
        catch
        {
            throw;
        }
        return Page();
    }
}
