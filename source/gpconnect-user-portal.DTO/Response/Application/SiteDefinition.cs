using System;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Response.Application
{
    public class SiteDefinition
    {
        public string SiteAsid { get; set; }
        public string SitePartyKey { get; set; }
        public string SiteOdsCode { get; set; }
        public int SiteDefinitionId { get; set; }
        public Guid SiteUniqueIdentifier { get; set; }
        public int SiteDefinitionStatusId { get; set; }
        public bool CanUpdateOrSubmit { get; set; }

        public List<SiteAttribute> SiteAttributes { get; set; }
    }
}
