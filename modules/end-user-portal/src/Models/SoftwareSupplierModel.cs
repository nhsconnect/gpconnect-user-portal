
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SoftwareSupplierModel : BaseModel
{
    [Display(Name = "SoftwareSupplierName", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SoftwareSupplierName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SelectedSoftwareSupplierName { get; set; } = "";

    public List<SoftwareSupplierResult> SoftwareSupplierNameList { get; set; } = new List<SoftwareSupplierResult>();

    public bool DisplaySoftwareSupplierProducts { get; set; } = false;

    [Display(Name = "SoftwareSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    public string? SelectedSoftwareSupplierProduct { get; set; }

    public List<SoftwareSupplierProductResult> SoftwareSupplierProductList { get; set; } = new List<SoftwareSupplierProductResult>();
}
