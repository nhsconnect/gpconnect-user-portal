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
        PrepopulateSoftwareSupplier();
        return Page();
    }

    private void PrepopulateSoftwareSupplier()
    {
        var selectedSoftwareSupplierNameId = TempData.Get<SoftwareSupplierResult>("SelectedSoftwareSupplierName");
        SelectedSoftwareSupplierNameId = selectedSoftwareSupplierNameId.SoftwareSupplierId;
        SoftwareSupplierProductList = TempData.Get<List<SoftwareSupplierProductResult>>("SelectedSoftwareSupplierProduct");
        DisplaySoftwareSupplierProducts = SoftwareSupplierProductList != null;
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
        return LoadSoftwareSupplierProducts(SelectedSoftwareSupplierNameId);
    }

    public IActionResult OnPostNextAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        TempData.Put("SelectedSoftwareSupplierName", SelectedSoftwareSupplier);
        TempData.Put("SelectedSoftwareSupplierProduct", SoftwareSupplierProductList);
        return Redirect("./Organisation");
    }

    private IActionResult LoadSoftwareSupplierProducts(int selectedSoftwareSupplier)
    {
        var supplierProducts = new List<SoftwareSupplierProductResult>() {
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 1, SoftwareSupplierProduct = "Access Record: HTML" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 2, SoftwareSupplierProduct = "Access Record: Structured" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 3, SoftwareSupplierProduct = "Appointment Management" },
            new SoftwareSupplierProductResult() { SoftwareSupplierProductId = 4, SoftwareSupplierProduct = "Send Document" }
        };
        TempData.Put("SoftwareSupplierProductList", supplierProducts);
        SoftwareSupplierProductList = supplierProducts;
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierNameId");
    }
}
