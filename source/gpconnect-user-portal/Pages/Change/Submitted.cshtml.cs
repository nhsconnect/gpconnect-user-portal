using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Pages
{
    public partial class SubmittedModel : BaseModel
    {
        private readonly ILogger<SubmittedModel> _logger;

        public SubmittedModel(ILogger<SubmittedModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
