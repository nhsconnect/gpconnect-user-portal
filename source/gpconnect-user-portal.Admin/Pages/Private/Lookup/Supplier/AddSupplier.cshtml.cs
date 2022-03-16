using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierModel : BaseSiteModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SupplierModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostSaveSupplierDetailsAsync()
        {
            if (ModelState.IsValid)
            {
                await _aggregateService.ReferenceService.AddSupplier(SupplierName);
                return LocalRedirect($"~/Lookup/Supplier");
            }
            return Page();
        }
    }
}
