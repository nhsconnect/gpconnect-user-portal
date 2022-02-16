using gpconnect_user_portal.Admin.Models;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class CompletedModel : BaseModel
    {
        private readonly ILogger<CompletedModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public CompletedModel(ILogger<CompletedModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
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
                    SiteDefinitionStatusIdLowerBand = (int)SiteDefinitionStatus.Completed,
                    SiteDefinitionStatusIdUpperBand = (int)SiteDefinitionStatus.Completed
                }
               );
        }
    }
}
