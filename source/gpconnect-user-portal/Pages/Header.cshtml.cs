using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class HeaderModel : BaseModel
    {
        private readonly ILogger<HeaderModel> _logger;
        private readonly IAggregateService _aggregateService;

        public HeaderModel(ILogger<HeaderModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {
        }
    }
}
