using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
    private const int FIRST_PAGE = 1;
    private readonly ISiteService _siteService;
    private readonly IOptions<ResultPageConfig> _config;

    public ResultsModel(IOptions<ResultPageConfig> config, IOptions<ApplicationParameters> applicationParameters, ISiteService siteService) : base(applicationParameters)
    {
        _siteService = siteService;
        _config = config;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!ModelState.IsValid) 
        {
            return RedirectToPage("./Name");
        }

        // Check Page Number not less than 1
        if (PageNumber < FIRST_PAGE)
        {
            return RedirectToPage("./Results", new { query = Query, mode = Mode });
        }

        try
        {
            SearchResult = await _siteService.SearchSitesAsync(Query, Mode, (PageNumber - 1) * _config.Value.ResultsPerPage, _config.Value.ResultsPerPage);

            if (SearchResult.TotalResults == 0)
            {
                return RedirectToPage("./NoResults", new { query = Query, mode = Mode });
            }

            if (SearchResult.TotalResults == 1)
            {
                return RedirectToPage("./Detail", new { 
                    id = SearchResult.SearchResults[0].SiteDefinitionId, 
                    query = Query, 
                    mode = Mode,
                    page = PageNumber
                });
            }

            if (PageNumber > NumPages)
            {
                return RedirectToPage("./Results", new { query = Query, mode = Mode, pageNumber = NumPages});
            }

            SearchResult.SearchResults.Sort((x, y) => x.SiteName.CompareTo(y.SiteName));
            return Page();
        }
        catch
        {
            throw;
        }
    }

    public BackPartialModel BackPartial => new BackPartialModel
    {
        Query = Query,
        Source = DetailViewSource.Search,
        Mode = Mode
    };
    
    public int NumPages => (int)Math.Ceiling((decimal)SearchResult.TotalResults / _config.Value.ResultsPerPage);
    public int CurrentPageNumber => PageNumber;
    public int TotalResults => SearchResult.TotalResults;
    public bool ShowNextLink => CurrentPageNumber < NumPages;
    public bool ShowPreviousLink => CurrentPageNumber > FIRST_PAGE;
    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;
    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
