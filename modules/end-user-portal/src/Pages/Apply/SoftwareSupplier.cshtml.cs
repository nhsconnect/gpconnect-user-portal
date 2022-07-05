using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SoftwareSupplierModel : BaseModel
{
    private readonly ISupplierService _supplierService;
    private readonly ITempDataProviderService _tempDataProviderService;

    public SoftwareSupplierModel(IOptions<ApplicationParameters> applicationParameters, ISupplierService supplierService, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _supplierService = supplierService;
        _tempDataProviderService = tempDataProviderService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ClearModelState();
        PrepopulateSoftwareSupplier();
        await GetSoftwareSupplierNameList();        
        return Page();
    }

    private void PrepopulateSoftwareSupplier()
    {
        if (IsSelectedSoftwareSupplier)
        {
            var selectedSoftwareSupplierNameId = _tempDataProviderService.GetItem<SoftwareSupplierResult>("SelectedSoftwareSupplierName");
            SelectedSoftwareSupplierNameId = selectedSoftwareSupplierNameId.SoftwareSupplierId;
            GpConnectInteractionForSupplierList = _tempDataProviderService.GetItem<List<GpConnectInteractionForSupplier>>("SelectedGpConnectInteractionForSupplier");
            DisplayGpConnectInteractionForSupplierList = GpConnectInteractionForSupplierList != null;
        }
    }

    protected async Task GetSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        _tempDataProviderService.PutItem("SoftwareSupplierNameList", suppliers);
    }

    public IActionResult OnPostCheckGpConnectInteractionForSupplierListAsync()
    {        
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        DisplayGpConnectInteractionForSupplierList = true;
        LoadGpConnectInteractionForSupplier(SelectedSoftwareSupplierNameId);
        GpConnectInteractionForSupplierList = _tempDataProviderService.GetItem<List<GpConnectInteractionForSupplier>>("GpConnectInteractionForSupplierList");
        return Page();
    }

    public IActionResult OnPostNextAsync()
    {
        CheckSoftwareSupplierProductSelection();
        if (!ModelState.IsValid)
        {
            DisplayGpConnectInteractionForSupplierList = true;
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        _tempDataProviderService.PutItem("SelectedSoftwareSupplierName", SelectedSoftwareSupplier);
        _tempDataProviderService.PutItem("SelectedGpConnectInteractionForSupplier", GpConnectInteractionForSupplierList);       

        return RedirectToPage("./Organisation");
    }

    private void CheckSoftwareSupplierProductSelection()
    {
        if(!GpConnectInteractionForSupplierList.Any(x => x.Selected))
        {
            ModelState.AddModelError("HasSelectedGpConnectInteractionForSupplier", ErrorMessageResources.GpConnectInteractionForSupplier);
        }
    }

    private IActionResult LoadGpConnectInteractionForSupplier(int selectedSoftwareSupplier)
    {
        var gpConnectInteractionsForSupplier = new List<GpConnectInteractionForSupplier>() {
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 1, GpConnectInteractionForSupplierValue = "Access Record: HTML" },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 2, GpConnectInteractionForSupplierValue = "Access Record: Structured" },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 3, GpConnectInteractionForSupplierValue = "Appointment Management" },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 4, GpConnectInteractionForSupplierValue = "Send Document" }
        };

        GpConnectInteractionForSupplierList = gpConnectInteractionsForSupplier;
        _tempDataProviderService.PutItem("GpConnectInteractionForSupplierList", gpConnectInteractionsForSupplier);
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierNameId");
    }
}
