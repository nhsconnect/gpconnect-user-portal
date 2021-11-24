using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace gpconnect_user_portal.Models
{
    public abstract class BaseModel : PageModel
    {
        private readonly IAggregateService _aggregateService;

        protected BaseModel(IAggregateService aggregateService)
        {
            _aggregateService = aggregateService;
        }

        public string ApplicationName => _aggregateService.ApplicationService.GetApplicationDetails().ApplicationName;
        public string AssemblyName => _aggregateService.ApplicationService.GetApplicationDetails().AssemblyName;
        public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";

        

        public HtmlString GetAccessEmailAddressLink => new HtmlString($"<a href=\"mailto:{_aggregateService.ApplicationService.GetApplicationDetails().ApplicationEmailAddress}\">{_aggregateService.ApplicationService.GetApplicationDetails().ApplicationEmailAddress}</a>");
    }
}