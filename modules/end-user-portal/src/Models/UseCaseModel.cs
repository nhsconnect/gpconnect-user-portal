using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class UseCaseModel : BaseModel
{
    [Display(Name = "UseCaseDescription", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "UseCaseDescription", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string UseCaseDescription { get; set; } = "";
}
