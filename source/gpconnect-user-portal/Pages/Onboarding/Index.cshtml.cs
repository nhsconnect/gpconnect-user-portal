using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class OnboardingModel : BaseModel
    {
        private readonly ILogger<OnboardingModel> _logger;
        private readonly IAggregateService _aggregateService;

        public string ContentPage { get; set; }

        public OnboardingModel(ILogger<OnboardingModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet([FromRoute] string contentPage = "Home")
        {
            ContentPage = contentPage;
        }
    }
}
