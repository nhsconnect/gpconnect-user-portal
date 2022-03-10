using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierProductModel : BaseSiteModel
    {
        private readonly ILogger<SupplierProductModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SupplierProductModel(ILogger<SupplierProductModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(int supplierId)
        {
            await PopulateForm(supplierId);
            return Page();
        }

        private async Task<IActionResult> PopulateForm(int supplierId)
        {
            SelectedSupplier = supplierId;
            Suppliers = await GetDropDown((int)Services.Enumerations.LookupType.Supplier, supplierId);
            return Page();
        }

        private async Task<IEnumerable<SelectListItem>> GetDropDown(int lookupTypeId, int selectedOption = 0)
        {
            var lookup = await _aggregateService.ReferenceService.GetLookup(lookupTypeId);
            var options = lookup.Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedOption == option.LookupId }).ToList();
            return options;
        }
    }
}
