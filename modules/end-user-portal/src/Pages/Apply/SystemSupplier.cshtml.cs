using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SystemSupplierModel : BaseModel
{
    private readonly ISupplierService _supplierService;
    private readonly ITempDataProviderService _tempDataProviderService;

    public SystemSupplierModel(IOptions<ApplicationParameters> applicationParameters, ISupplierService supplierService, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
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
            var selectedSoftwareSupplierNameId = _tempDataProviderService.GetItem<SoftwareSupplierResult>(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAME);
            SelectedSoftwareSupplierNameId = selectedSoftwareSupplierNameId.SoftwareSupplierId;
            GpConnectInteractionForSupplierList = _tempDataProviderService.GetItem<List<GpConnectInteractionForSupplier>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER);
            DisplayGpConnectInteractionForSupplierList = GpConnectInteractionForSupplierList != null;
        }
    }

    public async Task GetSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        _tempDataProviderService.PutItem(TempDataConstants.SOFTWARESUPPLIERNAMELIST, suppliers);
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
        GpConnectInteractionForSupplierList = _tempDataProviderService.GetItem<List<GpConnectInteractionForSupplier>>(TempDataConstants.GPCONNECTINTERACTIONFORSUPPLIERLIST);
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

        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAME, SelectedSoftwareSupplier);
        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER, GpConnectInteractionForSupplierList);       

        return RedirectToPage("./Organisation");
    }

    private void CheckSoftwareSupplierProductSelection()
    {
        if(!GpConnectInteractionForSupplierList.Any(x => x.Selected))
        {
            ModelState.AddModelError(TempDataConstants.HASSELECTEDGPCONNECTINTERACTIONFORSUPPLIER, ErrorMessageResources.GpConnectInteractionForSupplier);
        }
    }

    private IActionResult LoadGpConnectInteractionForSupplier(int selectedSoftwareSupplier)
    {
        var gpConnectInteractionsForSupplier = new List<GpConnectInteractionForSupplier>() {
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 1, GpConnectInteractionForSupplierValue = AccessRecordHTML },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 2, GpConnectInteractionForSupplierValue = AccessRecordStructured },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 3, GpConnectInteractionForSupplierValue = AppointmentManagement },
            new GpConnectInteractionForSupplier() { GpConnectInteractionForSupplierId = 4, GpConnectInteractionForSupplierValue = SendDocument }
        };

        GpConnectInteractionForSupplierList = gpConnectInteractionsForSupplier;
        _tempDataProviderService.PutItem(TempDataConstants.GPCONNECTINTERACTIONFORSUPPLIERLIST, gpConnectInteractionsForSupplier);
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierNameId");
    }
}
