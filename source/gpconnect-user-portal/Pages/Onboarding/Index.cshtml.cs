using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Pages
{
    public class OnboardingModel : BaseModel
    {
        private readonly ILogger<OnboardingModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public string ContentPage { get; set; }

        public OnboardingModel(ILogger<OnboardingModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService; ;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public void OnGet([FromRoute] string contentPage = "Home")
        {
            ContentPage = contentPage;
        }
    }
}
