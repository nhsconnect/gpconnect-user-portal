using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierProductModel : BaseSiteModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SupplierProductModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(int supplierId = 0)
        {
            ModelState.Clear();
            SelectedSupplier = supplierId;
            var supplier = await _aggregateService.ReferenceService.GetLookupById(supplierId);
            if(supplier != null)
            {
                SupplierName = supplier.LookupValue;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSaveSupplierProductDetailsAsync()
        {
            if (ModelState.IsValid)
            {
                var supplierProduct = new DTO.Request.Reference.SupplierProduct() {
                    SupplierId = SelectedSupplier,
                    ProductName = ProductName,
                    ProductUseCase = ProductUseCase
                };
                await _aggregateService.ReferenceService.AddProduct(supplierProduct);
                return LocalRedirect($"~/Lookup/Supplier");
            }
            return Page();
        }
    }
}
