using System;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Site
    {
        public Guid SiteUniqueIdentifier { get; set; }
        public string OdsCode { get; set; }
        public string SpineASID { get; set; }
        public string PartyKey { get; set; }
        public string SupplierOdsCode { get; set; }
        public string SupplierName { get; set; }
        public string SiteName { get; set; }
        public string CCGOdsCode { get; set; }
        public string CCGOdsName { get; set; }
        public string ServiceInteractions { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNumber { get; set; }
    }
}