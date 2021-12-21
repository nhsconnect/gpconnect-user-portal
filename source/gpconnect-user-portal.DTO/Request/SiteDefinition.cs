using System;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Request
{
    public class SiteDefinition
    {
        public string SiteOdsCode { get; set; }
        public string SiteAsid { get; set; }
        public string SitePartyKey { get; set; }
        public List<SiteAttribute> SiteAttributes { get; set; }
    }
}
