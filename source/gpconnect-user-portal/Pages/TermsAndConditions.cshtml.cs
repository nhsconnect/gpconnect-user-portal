using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class TermsAndConditionsModel : BaseModel
    {
        private readonly ILogger<TermsAndConditionsModel> _logger;
        private readonly IAggregateService _aggregateService;

        public TermsAndConditionsModel(ILogger<TermsAndConditionsModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {
        }
    }
}