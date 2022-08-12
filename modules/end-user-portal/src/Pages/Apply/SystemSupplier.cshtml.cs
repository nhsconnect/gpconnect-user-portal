using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        await PopulateSoftwareSupplierNameList();
        return Page();
    }

    private void PrepopulateSoftwareSupplier()
    {
        if (IsSelectedSoftwareSupplier)
        {
            SelectedSoftwareSupplierId = _tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDSOFTWARESUPPLIERID);
            DisplayGpConnectInteractionForSupplierList = GpConnectInteractionForSupplierList != null;
            PopulateGpConnectInteractionForSupplierList();
        }
    }

    public async Task PopulateSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        SoftwareSupplierResultList = suppliers;
    }

    public async Task<IActionResult> OnPostCheckGpConnectInteractionForSupplierListAsync()
    {
        await PopulateSoftwareSupplierNameList();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        DisplayGpConnectInteractionForSupplierList = true;
        PopulateGpConnectInteractionForSupplierList();
        return Page();
    }

    public async Task<IActionResult> OnPostNextAsync()
    {
        await PopulateSoftwareSupplierNameList();
        CheckSoftwareSupplierProductSelection();
        if (!ModelState.IsValid)
        {
            DisplayGpConnectInteractionForSupplierList = true;
            return Page();
        }

        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDSOFTWARESUPPLIERID, SelectedSoftwareSupplierId);
        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER, GpConnectInteractionForSupplierList.Where(x => x.Selected).Select(x => (GpConnectInteractions)x.Id).ToList());

        return RedirectToPage("./Organisation");
    }

    public void PopulateGpConnectInteractionForSupplierList()
    {
        var selectedInteractions = _tempDataProviderService.GetItem<List<GpConnectInteractions>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER);
        var gpConnectInteractionForSupplierList = new List<GpConnectInteractionForSupplier>() {
            new GpConnectInteractionForSupplier() { Id = (int)GpConnectInteractions.AccessRecordHTML, Value = GpConnectInteractions.AccessRecordHTML.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Contains(GpConnectInteractions.AccessRecordHTML)},
            new GpConnectInteractionForSupplier() { Id = (int)GpConnectInteractions.AccessRecordStructured, Value = GpConnectInteractions.AccessRecordStructured.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Contains(GpConnectInteractions.AccessRecordStructured) },
            new GpConnectInteractionForSupplier() { Id = (int)GpConnectInteractions.AppointmentManagement, Value = GpConnectInteractions.AppointmentManagement.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Contains(GpConnectInteractions.AppointmentManagement) },
            new GpConnectInteractionForSupplier() { Id = (int)GpConnectInteractions.SendDocument, Value = GpConnectInteractions.SendDocument.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Contains(GpConnectInteractions.SendDocument) }
        };
        GpConnectInteractionForSupplierList = gpConnectInteractionForSupplierList;
    }

    private void CheckSoftwareSupplierProductSelection()
    {
        if (!GpConnectInteractionForSupplierList.Any(x => x.Selected))
        {
            ModelState.AddModelError(TempDataConstants.HASSELECTEDGPCONNECTINTERACTIONFORSUPPLIER, ErrorMessageResources.GpConnectInteractionForSupplier);
        }
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierId");
    }
}
