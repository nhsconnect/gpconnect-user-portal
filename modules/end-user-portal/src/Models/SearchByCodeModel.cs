using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class SearchByCodeModel : BaseModel
{
    [Display(Name = "ProviderOdsCode", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ProviderOdsCode", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string? ProviderOdsCode { get; set; } = "";
}
