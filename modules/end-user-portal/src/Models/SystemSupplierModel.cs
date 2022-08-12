using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SystemSupplierModel : BaseModel
{
    [Display(Name = "SoftwareSupplierName", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "SoftwareSupplierName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string SelectedSoftwareSupplierId { get; set; } = "";

    public List<SoftwareSupplierResult> SoftwareSupplierResultList { get; set; }

    public bool IsSelectedSoftwareSupplier => !string.IsNullOrEmpty(_tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDSOFTWARESUPPLIERID));

    [BindProperty(SupportsGet = true)]
    public bool DisplayGpConnectInteractionForSupplierList { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<GpConnectInteractionForSupplier> GpConnectInteractionForSupplierList { get; set; }

    [Display(Name = "GpConnectInteractionForSupplier", ResourceType = typeof(DataFieldNameResources))]
    public bool HasSelectedGpConnectInteractionForSupplier { get; set; }
}
