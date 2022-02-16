using gpconnect_user_portal.Admin.Models;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class OutstandingModel : BaseModel
    {
        private readonly ILogger<OutstandingModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public OutstandingModel(ILogger<OutstandingModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task OnGet()
        {
            EndpointChanges = await _aggregateService.ApplicationService.GetEndpointChanges(
                new DTO.Request.EndpointChange
                {
                    SiteDefinitionStatusIdLowerBand = (int)SiteDefinitionStatus.AwaitingReview,
                    SiteDefinitionStatusIdUpperBand = (int)SiteDefinitionStatus.AwaitingSpineUpdate
                }
               );
        }
    }
}
