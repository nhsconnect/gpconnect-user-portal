using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;

namespace gpconnect_user_portal.Models
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

        public string ApplicationName => _generalOptionsDelegate.CurrentValue.ProductName;
        public string AssemblyName => _aggregateService.ApplicationService.GetApplicationDetails().AssemblyName;
        public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";        

        public HtmlString GetAccessEmailAddressLink => new HtmlString($"<a href=\"mailto:{_aggregateService.ApplicationService.GetApplicationDetails().ApplicationEmailAddress}\">{_aggregateService.ApplicationService.GetApplicationDetails().ApplicationEmailAddress}</a>");
    }
}