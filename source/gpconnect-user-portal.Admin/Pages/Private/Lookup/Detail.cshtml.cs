using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class LookupDetailModel : BaseSiteModel
    {
        private readonly ILogger<LookupDetailModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public LookupDetailModel(ILogger<LookupDetailModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(string lookupType)
        {
            var lookupEnum = lookupType.ToEnum<Services.Enumerations.LookupType>();
            return await PopulatePage((int)lookupEnum);
        }

        private async Task<IActionResult> PopulatePage(int lookupTypeId)
        {
            var lookups = await _aggregateService.ReferenceService.GetLookup(lookupTypeId);
            if (lookups != null && lookups.Count > 0)
            {
                Lookups = lookups;
                LookupName = lookups?.FirstOrDefault()?.LookupTypeDescription;
                LookupTypeId = lookupTypeId;
                return Page();
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> OnPostDisableLookupAsync(int lookupTypeId, int lookupId)
        {
            await _aggregateService.ReferenceService.EnableDisableLookup(lookupId, true);
            return await PopulatePage(lookupTypeId);
        }

        public IActionResult OnPostAddLookup()
        {
            return LocalRedirect($"~/Lookup/AddLookup/{LookupTypeId}");
        }

        public async Task<IActionResult> OnPostEnableLookupAsync(int lookupTypeId, int lookupId)
        {
            await _aggregateService.ReferenceService.EnableDisableLookup(lookupId, false);
            return await PopulatePage(lookupTypeId);
        }

        public async Task<IActionResult> OnPostUpdateLookupAsync(int lookupTypeId, int lookupId)
        {
            ModelState.ClearValidationState("UpdateLookupValue");
            ModelState.MarkFieldValid("UpdateLookupValue");
            UpdateLookupId = lookupId;
            return await PopulatePage(lookupTypeId);
        }

        public async Task<IActionResult> OnPostSaveLookupAsync(int lookupTypeId, int lookupId)
        {
            if (ModelState.IsValid)
            {
                UpdateLookupId = null;
                await _aggregateService.ReferenceService.UpdateLookup(lookupId, UpdateLookupValue);                
            }
            return await PopulatePage(lookupTypeId);
        }
    }
}
