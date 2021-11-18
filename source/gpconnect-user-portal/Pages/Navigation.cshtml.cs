using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_appointment_checker.Pages
{
    public class NavigationModel : BaseModel
    {
        private readonly ILogger<NavigationModel> _logger;
        private readonly IAggregateService _aggregateService;

        public NavigationModel(ILogger<NavigationModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {
        }
    }
}
