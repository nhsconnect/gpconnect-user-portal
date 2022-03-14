using gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces;
using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Pages
{
    public class IndexModel : BaseModel
    {
        private readonly ILoggerManager _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public IndexModel(ILoggerManager logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public void OnGet()
        {
            _logger.LogInformation("Hello world");
        }
    }
}
