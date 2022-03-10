using gpconnect_user_portal.DTO.Request.Reference;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierDetailModel : BaseSiteModel
    {
        private readonly ILogger<SupplierDetailModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SupplierDetailModel(ILogger<SupplierDetailModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SupplierProductCapabilityModel = new Models.SupplierProductCapabilityModel();
            GetSuppliers();
            return Page();
        }

        private void GetSuppliers()
        {
            var suppliers = GetDropDown(Services.Enumerations.LookupType.Supplier, SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplier);
            SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.Suppliers = suppliers;
        }

        public async Task<IActionResult> OnPostLoadSupplierProductsAsync()
        {
            GetSuppliers();
            await GetSupplierProducts();
            return Page();
        }

        public async Task<IActionResult> OnPostLoadSupplierCapabilitiesAsync()
        {
            ModelState.Clear();
            GetSuppliers();
            await GetSupplierProducts();
            await GetSupplierProductCapabilities();
            return Page();
        }

        private async Task GetSupplierProductCapabilities()
        {
            var selectedSupplierProduct = SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplierProduct;

            if (selectedSupplierProduct != null && selectedSupplierProduct > 0)
            {
                var supplierProductCapabilities = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(selectedSupplierProduct.Value, true);
                var capabilityList = new List<Models.SupplierProductCapabilityDetailsModel>();

                foreach (var supplierProductCapability in supplierProductCapabilities.SupplierProductCapability)
                {
                    capabilityList.Add(new Models.SupplierProductCapabilityDetailsModel()
                    {
                        Name = supplierProductCapability.LookupValueAssurance,
                        AssuranceDate = supplierProductCapability.AssuranceDate,
                        AwaitingAssurance = supplierProductCapability.AwaitingAssurance,
                        CapabilityVersion = supplierProductCapability.CapabilityVersion,
                        ProviderAssured = supplierProductCapability.ProviderAssured,
                        ConsumerAssured = supplierProductCapability.ConsumerAssured,
                        SupplierProductCapabilityId = supplierProductCapability.SupplierProductCapabilityId,
                        ProductCapabilityId = supplierProductCapability.ProductCapabilityId,
                        SupplierProductId = supplierProductCapability.SupplierProductId,
                        SupplierId = supplierProductCapability.SupplierId
                    });
                }
                SupplierProductCapabilityModel.SupplierProductCapabilityDetailsModel = capabilityList;
            }
            SupplierProductCapabilityModel.DisplayCapabilities = true;
        }

        private async Task GetSupplierProducts()
        {
            var selectedSupplier = SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplier;
            var supplierProducts = await _aggregateService.ReferenceService.GetSupplierProducts(selectedSupplier);
            var options = supplierProducts.Select(option => new SelectListItem() { Text = option.ProductName, Value = option.SupplierProductId.ToString() }).ToList();
            if (options.Any())
            {
                options.Insert(0, new SelectListItem());
            }
            SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SupplierProducts = options;
            SupplierProductCapabilityModel.DisplaySupplierProducts = true;
        }

        public async Task<IActionResult> OnPostSaveChangesAsync()
        {
            if (ModelState.IsValid)
            {
                await _aggregateService.ReferenceService.UpdateSupplierProductCapabilities(SupplierProductCapabilityModel.ConvertObjectToJsonData());
            }
            //await PopulateForm();
            GetSuppliers();
            await GetSupplierProducts();
            await GetSupplierProductCapabilities();
            return Page();
        }

        //private async Task<IActionResult> PopulateForm()
        //{
        //    SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.Suppliers = GetDropDown(Services.Enumerations.LookupType.Supplier, SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplier);
        //    var supplierProducts = await _aggregateService.ReferenceService.GetSupplierProducts(SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplier);
        //    var options = supplierProducts.Select(option => new SelectListItem() { Text = option.ProductName, Value = option.SupplierProductId.ToString() }).ToList();
        //    if (options.Any())
        //    {
        //        options.Insert(0, new SelectListItem());
        //    }
        //    SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SupplierProducts = options;

        //    if (SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplierProduct != null && SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplierProduct > 0)
        //    {
        //        var supplierProductCapabilities = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(SupplierProductCapabilityModel.SupplierProductCapabilitySupplierModel.SelectedSupplierProduct.Value);
        //        return PopulateSupplierCapabilities(supplierProductCapabilities.SupplierProductCapability);
        //    }
        //    return Page();
        //}

        private IEnumerable<SelectListItem> GetDropDown(Services.Enumerations.LookupType lookupType, int selectedOption = 0)
        {
            var lookup = _aggregateService.ReferenceService.GetLookup((int)lookupType).Result;
            var options = lookup.Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedOption == option.LookupId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }
    }
}
