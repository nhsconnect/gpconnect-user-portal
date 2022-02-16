using System;

namespace gpconnect_user_portal.DTO.Request
{
    public class EndpointChange
    {
        public string SearchValue { get; set; }
        public DateTime? SearchDateFrom { get; set; }
        public DateTime? SearchDateTo { get; set; }
        public int SiteDefinitionStatusIdLowerBand { get; set; }
        public int SiteDefinitionStatusIdUpperBand { get; set; }
    }
}
