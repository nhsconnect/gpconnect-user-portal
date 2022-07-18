using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class OrganisationModel : BaseModel
{
    [Display(Name = "SiteOdsCode", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SiteOdsCode", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SiteOdsCode { get; set; } = "";

    public bool IsSelectedOrganisation => !string.IsNullOrEmpty(_tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDORGANISATIONODSCODE));

    public bool OrganisationFound { get; set; } = false;

    public OrganisationResult OrganisationResult { get; set; } = new OrganisationResult();
}
