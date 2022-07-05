using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SoftwareSupplierModel : BaseModel
{
    private readonly ISupplierService _supplierService;

    public SoftwareSupplierModel(IOptions<ApplicationParameters> applicationParameters, ISupplierService supplierService) : base(applicationParameters)
    {
        _supplierService = supplierService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ClearModelState();
        await GetSoftwareSupplierNameList();
        return Page();
    }

    protected async Task GetSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        TempData.Put("SoftwareSupplierNameList", suppliers);
    }

    public IActionResult OnPostCheckSupplierProductsAsync()
    {        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        DisplaySoftwareSupplierProducts = true;
        return LoadSoftwareSupplierProducts(SelectedSoftwareSupplierName);
    }

    public IActionResult OnPostNextAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        TempData.Put("SelectedSoftwareSupplierName", SelectedSoftwareSupplierName);
        TempData.Put("SelectedSoftwareSupplierProduct", SelectedSoftwareSupplierProduct);
        return Redirect("./Organisation");
    }

    private IActionResult LoadSoftwareSupplierProducts(string selectedSoftwareSupplierName)
    {
        SoftwareSupplierProductList = new List<SoftwareSupplierProductResult>() {
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 1, SoftwareSupplierProduct = "Access Record: HTML" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 2, SoftwareSupplierProduct = "Access Record: Structured" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 3, SoftwareSupplierProduct = "Appointment Management" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 4, SoftwareSupplierProduct = "Send Document" }
        };
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierName");
    }
}
