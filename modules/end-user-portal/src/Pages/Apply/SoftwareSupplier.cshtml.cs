using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants.GpConnectInteractions;

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
            SelectedSoftwareSupplierNameId = _tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAMEID);
            DisplayGpConnectInteractionForSupplierList = GpConnectInteractionForSupplierList != null;
            GetGpConnectInteractionForSupplierList();
        }
    }

    public async Task GetSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        SoftwareSupplierResultList = suppliers;
    }

    public async Task<IActionResult> OnPostCheckGpConnectInteractionForSupplierListAsync()
    {
        await GetSoftwareSupplierNameList();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        DisplayGpConnectInteractionForSupplierList = true;
        GetGpConnectInteractionForSupplierList();

        return Page();

    }

    public async Task<IActionResult> OnPostNextAsync()
    {
        await GetSoftwareSupplierNameList();
        CheckSoftwareSupplierProductSelection();
        if (!ModelState.IsValid)
        {
            DisplayGpConnectInteractionForSupplierList = true;
            return Page();
        }

        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAMEID, SelectedSoftwareSupplierNameId);
        _tempDataProviderService.PutItem(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER, GpConnectInteractionForSupplierList.Where(x => x.Selected).Select(x => x.Id).ToList());

        return RedirectToPage("./Organisation");
    }

    private void CheckSoftwareSupplierProductSelection()
    {
        if(!GpConnectInteractionForSupplierList.Any(x => x.Selected))
        {
            ModelState.AddModelError(TempDataConstants.HASSELECTEDGPCONNECTINTERACTIONFORSUPPLIER, ErrorMessageResources.GpConnectInteractionForSupplier);
        }
    }

    public void GetGpConnectInteractionForSupplierList()
    {
        var selectedInteractions = _tempDataProviderService.GetItem<List<int>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER);

        var gpConnectInteractionForSupplierList = new List<GpConnectInteractionForSupplier>() {
            new GpConnectInteractionForSupplier() { Id = (int)AccessRecordHTML, Value = AccessRecordHTML.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Any(x => x == (int)AccessRecordHTML)},
            new GpConnectInteractionForSupplier() { Id = (int)AccessRecordStructured, Value = AccessRecordStructured.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Any(x => x == (int)AccessRecordStructured) },
            new GpConnectInteractionForSupplier() { Id = (int)AppointmentManagement, Value = AppointmentManagement.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Any(x => x == (int)AppointmentManagement) },
            new GpConnectInteractionForSupplier() { Id = (int)SendDocument, Value = SendDocument.GetDisplayParameter(), Selected = selectedInteractions != null && selectedInteractions.Any(x => x == (int)SendDocument) }
        };
        GpConnectInteractionForSupplierList = gpConnectInteractionForSupplierList;
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierNameId");
    }
}
