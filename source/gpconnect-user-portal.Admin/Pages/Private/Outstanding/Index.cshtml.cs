using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class OutstandingEndpointChangesModel : BaseSiteModel
    {
        private readonly ILogger<OutstandingEndpointChangesModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public OutstandingEndpointChangesModel(ILogger<OutstandingEndpointChangesModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task OnGet()
        {
            var endpointChange = new EndpointChange()
            {
                SiteDefinitionStatusIdLowerBand = (int)SiteDefinitionStatus.AwaitingReview,
                SiteDefinitionStatusIdUpperBand = (int)SiteDefinitionStatus.AwaitingSpineUpdate
            };
            EndpointChanges = await _aggregateService.ApplicationService.GetEndpointChanges(endpointChange);
        }
    }
}