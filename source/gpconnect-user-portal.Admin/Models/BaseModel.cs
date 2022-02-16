using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Models
{
    public abstract class BaseModel : PageModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public string GetAccessEmailAddress => _generalOptionsDelegate.CurrentValue.GetAccessEmailAddress;
        public string GetAccessEmailAddressLink => $"<a href=\"mailto:{_generalOptionsDelegate.CurrentValue.GetAccessEmailAddress}\">{_generalOptionsDelegate.CurrentValue.GetAccessEmailAddress}</a>";
        public string ApplicationName => _generalOptionsDelegate.CurrentValue.AdminProductName;
        public string AssemblyName => _aggregateService.CoreService.GetApplicationDetails().AssemblyName;
        public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";
    }
}