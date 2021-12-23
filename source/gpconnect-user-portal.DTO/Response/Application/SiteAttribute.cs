using System;

namespace gpconnect_user_portal.DTO.Response.Application
{
    public class SiteAttribute
    {
        public int SiteAttributeId { get; set; }
        public int SiteDefinitionId { get; set; }
        public Guid SiteUniqueIdentifier { get; set; }
        public string SiteAttributeName { get; set; }
        public string SiteAttributeValue { get; set; }
        public string LookupValue { get; set; }
    }
}
