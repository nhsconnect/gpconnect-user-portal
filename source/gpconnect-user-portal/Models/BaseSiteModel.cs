using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Resources;

namespace gpconnect_user_portal.Pages
{
    public abstract class BaseSiteModel : BaseModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseSiteModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }
        
        public string SiteIdentifier { get; set; }

        public List<DTO.Response.Application.SiteAttribute> SiteAttributes { get; set; }

        public bool CanUpdateOrSubmit { get; set; }

        public T GetEnumValue<T>(string attributeName)
        {
            var attribute = SiteAttributes?.Find(x => x.SiteAttributeName == attributeName);
            if (attribute != null)
            {
                return (T)Enum.Parse(typeof(T), attribute.SiteAttributeValue, true);
            }
            return default(T);
        }

        public string GetAttributeValue(string attributeName, bool useLookupValue = false)
        {
            var attribute = SiteAttributes?.Find(x => x.SiteAttributeName == attributeName);
            if (attribute != null)
            {
                if (useLookupValue)
                {
                    return StringExtensions.Coalesce(attribute.LookupValue, attribute.SiteAttributeValue);
                }
                return StringExtensions.Coalesce(attribute.SiteAttributeValue, attribute.LookupValue);
            }
            return null;
        }

        public string GetAttributeName(string attributeName)
        {
            var attribute = SiteAttributes?.Find(x => x.SiteAttributeName == attributeName);
            if (attribute != null)
            {
                return attribute.SiteAttributeName;
            }
            return null;
        }
    }
}