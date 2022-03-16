using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class IndexModel : BaseModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public IndexModel(ILogger<IndexModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task OnGet()
        {
            EndpointChangeCountByStatus = await _aggregateService.ApplicationService.GetEndpointChangeCountByStatus();
            LookupDataCountByType = await _aggregateService.ReferenceService.GetLookupDataCountByType();
        }
    }
}
