using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class CompletedEndpointChangeDetailModel : BaseSiteModel
    {
        private readonly ILogger<CompletedEndpointChangeDetailModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public CompletedEndpointChangeDetailModel(ILogger<CompletedEndpointChangeDetailModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(string siteIdentifier)
        {
            var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(siteIdentifier);
            if (siteDefinition != null)
            {
                SiteIdentifier = siteIdentifier;
                SiteAttributes = siteDefinition.SiteAttributes;
                CanUpdateOrSubmit = siteDefinition.CanUpdateOrSubmit;

                return Page();
            }
            return new NotFoundResult();
        }
    }
}
