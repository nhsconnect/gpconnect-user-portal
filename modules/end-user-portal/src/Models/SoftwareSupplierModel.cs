using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
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
    public int SelectedSoftwareSupplierNameId { get; set; }

    public SoftwareSupplierResult SelectedSoftwareSupplier => TempData.Get<List<SoftwareSupplierResult>>("SoftwareSupplierNameList")?.FirstOrDefault(x => x.SoftwareSupplierId == SelectedSoftwareSupplierNameId);

    public bool DisplaySoftwareSupplierProducts { get; set; } = false;

    [BindProperty(SupportsGet = true)]
    public List<SoftwareSupplierProductResult> SoftwareSupplierProductList { get; set; } = new List<SoftwareSupplierProductResult>();

    //public IEnumerable<SoftwareSupplierProductResult> SelectedSoftwareSupplierProducts => SoftwareSupplierProductList);
}
