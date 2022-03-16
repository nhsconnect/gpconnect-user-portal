using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public class NavigationModel : BaseModel
    {
        private readonly ILogger<NavigationModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public NavigationModel(ILogger<NavigationModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public void OnGet()
        {
        }
    }
}
