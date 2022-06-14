using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class NoResultsModel : BaseModel
{
    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;

    public string SearchType => Mode == SearchMode.Name ? SearchMode.Name.GetNameParameter() : SearchMode.Code.GetNameParameter();

    [Display(Name = "OrganisationNameNoResults", ResourceType = typeof(DataFieldNameResources))]
    public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;

    [Display(Name = "OrganisationOdsCodeNoResults", ResourceType = typeof(DataFieldNameResources))]
    public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
