using System;

namespace gpconnect_user_portal.DTO.Request.Reference
{
    public class SupplierProductCapability
    {
        public int SupplierProductCapabilityId { get; set; }
        //public int SupplierProductId { get; set; }
        //public int ProductCapabilityId { get; set; }
        //public string LookupValue { get; set; }
        //public string LookupValueAssurance { get; set; }
        //public int SupplierId { get; set; }
        public bool ProviderAssured { get; set; }
        public bool ConsumerAssured { get; set; }
        public bool AwaitingAssurance { get; set; }
        public DateTime? AssuranceDate { get; set; }
        public string CapabilityVersion { get; set; }
    }
}
