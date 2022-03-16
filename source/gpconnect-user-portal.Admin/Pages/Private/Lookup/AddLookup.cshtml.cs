using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class LookupModel : BaseSiteModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public LookupModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(int lookupTypeId = 0)
        {
            ModelState.Clear();
            await GetLookupData(lookupTypeId);            
            return Page();
        }

        private async Task GetLookupData(int lookupTypeId)
        {
            var lookupType = await _aggregateService.ReferenceService.GetLookupType(lookupTypeId);
            if (lookupType != null)
            {
                LookupType = lookupType;
            }
        }

        public async Task<IActionResult> OnPostSaveLookupAsync()
        {
            await GetLookupData(LookupType.LookupTypeId);
            if (ModelState.IsValid)
            {
                var lookup = new DTO.Request.Reference.Lookup()
                {
                    LookupTypeId = LookupType.LookupTypeId,
                    LookupValue = LookupValue
                };
                await _aggregateService.ReferenceService.AddLookup(lookup);
                return LocalRedirect($"~/Lookup/{LookupType.LookupTypeName}");
            }
            return Page();
        }
    }
}
