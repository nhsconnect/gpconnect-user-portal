using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
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

    public async Task<IActionResult> OnGet(string id, string query, SearchMode mode)
    {
        try
        {
            var searchResultEntry = await _siteService.SearchSiteAsync(id);
            SearchResultEntry = searchResultEntry;
        }
        catch
        {
            throw;
        }
        return Page();
    }
}
