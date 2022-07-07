using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public class NoResultsModel : BaseModel
{
    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;

    public string SearchType => Mode == SearchMode.Name ? Mode.GetDisplayParameter() : Mode.GetDisplayParameter();

    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;

    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;

    public NoResultsModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }
}
