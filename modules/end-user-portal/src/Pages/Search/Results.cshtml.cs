using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
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

        if (PageNumber < PageConstants.FIRST_PAGE)
        {
            return RedirectToPage("./Results", new { query = Query, mode = Mode });
        }

        try
        {
            SearchResult = await _siteService.SearchSitesAsync(Query, Mode, ((PageNumber - 1) * _config.Value.ResultsPerPage) + 1, _config.Value.ResultsPerPage);

            if (SearchResult.TotalResults == 0)
            {
                return RedirectToPage("./NoResults", new { query = Query, mode = Mode });
            }

            if (PageNumber > NumPages)
            {
                return RedirectToPage("./Results", new { query = Query, mode = Mode, pageNumber = NumPages});
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

    public int CurrentPageNumber => PageNumber;

    public int TotalResults => SearchResult.TotalResults;

    public bool HasMoreResults => CurrentPageNumber < NumPages;

    public bool HasPreviousResults => CurrentPageNumber > PageConstants.FIRST_PAGE;
}
