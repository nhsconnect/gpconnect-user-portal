using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class DetailModel : BaseModel
{
    private readonly ISiteService _siteService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DetailModel(IOptions<ApplicationParameters> applicationParameters, ISiteService siteService, IHttpContextAccessor httpContextAccessor) : base(applicationParameters)
    {
        _siteService = siteService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> OnGet(string id, string query, SearchMode mode)
    {
        try
        {
            var searchResultEntry = await _siteService.SearchSiteAsync(id);
            SearchResultEntry = searchResultEntry;
            PopulateBack();
        }
        catch
        {
            throw;
        }
        return Page();
    }

    private void PopulateBack()
    {
        var source = DetailViewSource.Search;
        var mode = SearchMode.Name;
        StringValues query = StringValues.Empty;

        _httpContextAccessor?.HttpContext?.Request.Query.TryGetValue("Query", out query);
        if (_httpContextAccessor?.HttpContext?.Request.Query.TryGetValue("Source", out _) == true)
        {
            Enum.TryParse(_httpContextAccessor?.HttpContext?.Request.Query["Source"][0].ToString(), out source);
        }

        if (_httpContextAccessor?.HttpContext?.Request.Query.TryGetValue("Source", out _) == true)
        {
            Enum.TryParse(_httpContextAccessor?.HttpContext?.Request.Query["Mode"][0].ToString(), out mode);
        }

        BackPartial.Mode = mode;
        BackPartial.Query = query.FirstOrDefault();
        BackPartial.Source = source;
    }
}
