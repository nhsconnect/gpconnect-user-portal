using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace gpconnect_user_portal.Admin.Pages
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

        public bool UserIsAuthenticated => GetUserAuthenticationStatus();
        public bool UserIsAuthenticatedButNotAuthorised => GetUserStatus();

        private bool GetUserStatus()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return identity != null && identity.IsAuthenticated && identity.FindFirst("IsAdmin")?.Value.StringToBoolean() == false;
        }

        private bool GetUserAuthenticationStatus()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return identity != null && identity.IsAuthenticated && identity.FindFirst("IsAdmin")?.Value.StringToBoolean() == true;
        }
    }
}