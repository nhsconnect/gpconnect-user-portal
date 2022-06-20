using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
    public SearchResult SearchResult { get; set; }

    public BackPartialModel BackPartial { get; set; } = new BackPartialModel();

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;

    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;

    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
