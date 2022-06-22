using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class DetailModel : BaseModel
{
    private readonly ISiteService _siteService;

    public DetailModel(IOptions<ApplicationParameters> applicationParameters, ISiteService siteService) : base(applicationParameters)
    {
        _siteService = siteService;
    }

    public async Task<IActionResult> OnGet(string id)
    {
        try
        {
            var searchResultEntry = await _siteService.SearchSiteAsync(id);

            if (searchResultEntry == null) 
            {
                return NotFound();
            }
            
            SearchResultEntry = searchResultEntry;
            return Page();
        }
        catch
        {
            throw;
        }
    }

    public BackPartialModel BackPartial => new BackPartialModel {
        Query = Query,
        Source = Source,
        Mode = Mode,
        ResultsPageNumber = ResultsPageNumber
    };
}
