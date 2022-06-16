using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class SearchByNameModel : BaseModel
{
    [Display(Name = "ProviderName", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ProviderName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string? ProviderName { get; set; } = "";
}
