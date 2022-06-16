using System.ComponentModel.DataAnnotations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class ResultsModel : BaseModel
{
    public SearchResult SearchResult { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;

    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;

    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
