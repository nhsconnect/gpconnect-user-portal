using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
    public SearchResult SearchResult { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;

    [FromQuery(Name = "PageNumber")]
    public int PageNumber { get; set; } = PageConstants.FIRST_PAGE;

    public int NumPages => (int)Math.Ceiling((decimal)SearchResult.TotalResults / _config.Value.ResultsPerPage);

    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;
    
    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
