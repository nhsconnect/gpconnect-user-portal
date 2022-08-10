using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SystemSupplierModel : BaseModel
{
    [Display(Name = "SoftwareSupplierName", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Range(1, int.MaxValue, ErrorMessageResourceName = "SoftwareSupplierName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public int SelectedSoftwareSupplierNameId { get; set; }

    public SoftwareSupplierResult SelectedSoftwareSupplier => _tempDataProviderService.GetItem<List<SoftwareSupplierResult>>("SoftwareSupplierNameList")?.FirstOrDefault(x => x.SoftwareSupplierId == SelectedSoftwareSupplierNameId);

    public List<SoftwareSupplierResult> SoftwareSupplierResultList => _tempDataProviderService.GetItem<List<SoftwareSupplierResult>>("SoftwareSupplierNameList");

    public bool IsSelectedSoftwareSupplier => _tempDataProviderService.GetItem<SoftwareSupplierResult>("SelectedSoftwareSupplierName") != null;

    [BindProperty(SupportsGet = true)]
    public bool DisplayGpConnectInteractionForSupplierList { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<GpConnectInteractionForSupplier> GpConnectInteractionForSupplierList { get; set; }

    [Display(Name = "GpConnectInteractionForSupplier", ResourceType = typeof(DataFieldNameResources))]
    public bool HasSelectedGpConnectInteractionForSupplier { get; set; }
}
