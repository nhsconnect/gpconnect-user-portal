using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class PrivacyAndCookiesModel : BaseModel
    {
        private readonly ILogger<PrivacyAndCookiesModel> _logger;
        private readonly IAggregateService _aggregateService;

        public PrivacyAndCookiesModel(ILogger<PrivacyAndCookiesModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {            
        }
    }
}