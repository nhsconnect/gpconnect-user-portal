using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SignatoryModel : BaseModel
{
    [Display(Name = "SignatoryName", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SignatoryName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SignatoryName { get; set; } = "";

    [Display(Name = "SignatoryRole", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SignatoryRole", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SignatoryRole { get; set; } = "";

    [Display(Name = "SignatoryEmail", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SignatoryEmail", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SignatoryEmail { get; set; } = "";
}
