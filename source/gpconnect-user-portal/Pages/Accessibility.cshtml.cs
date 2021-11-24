using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class AccessibilityModel : BaseModel
    {
        private readonly ILogger<AccessibilityModel> _logger;
        private readonly IAggregateService _aggregateService;

        public AccessibilityModel(ILogger<AccessibilityModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {
        }
    }
}
