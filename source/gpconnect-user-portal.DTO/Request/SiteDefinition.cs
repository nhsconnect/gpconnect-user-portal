using System;

namespace gpconnect_user_portal.DTO.Request
{
    public class SiteDefinition
    {
        public Guid SiteUniqueIdentifier { get; set; }
        public string SiteOdsCode { get; set; }
        public string SiteAsid { get; set; }
        public string SitePartyKey { get; set; }
    }
}
