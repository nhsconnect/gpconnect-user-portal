using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
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
            SoftwareSupplierProductList = _tempDataProviderService.GetItem<List<SoftwareSupplierProductResult>>("SelectedSoftwareSupplierProduct");
            DisplaySoftwareSupplierProducts = SoftwareSupplierProductList != null;
        }
    }

    protected async Task GetSoftwareSupplierNameList()
    {
        var suppliers = await _supplierService.GetSoftwareSuppliersAsync();
        _tempDataProviderService.PutItem("SoftwareSupplierNameList", suppliers);
    }

    public IActionResult OnPostCheckSupplierProductsAsync()
    {        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (!_tempDataProviderService.HasItems)
        {
            return Redirect("./Timeout");
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

        if(!_tempDataProviderService.HasItems)
        {
            return Redirect("./Timeout");
        }

        _tempDataProviderService.PutItem("SelectedSoftwareSupplierName", SelectedSoftwareSupplier);
        _tempDataProviderService.PutItem("SelectedSoftwareSupplierProduct", SoftwareSupplierProductList);

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
        _tempDataProviderService.PutItem("SoftwareSupplierProductList", supplierProducts);
        SoftwareSupplierProductList = supplierProducts;
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SelectedSoftwareSupplierNameId");
    }
}
