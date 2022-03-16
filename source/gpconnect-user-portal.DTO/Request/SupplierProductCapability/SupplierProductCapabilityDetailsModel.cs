using System;

namespace gpconnect_user_portal.DTO.Request.Reference
{
    public class SupplierProductCapabilityDetailsModel
    {
        public int SupplierProductId { get; set; }
        public int SupplierId { get; set; }
        public int ProductCapabilityId { get; set; }
        public int SupplierProductCapabilityId { get; set;}
        public DateTime? AssuranceDate { get; set; }
        public bool AwaitingAssurance { get; set; }
        public bool ProviderAssured { get; set; }
        public bool ConsumerAssured { get; set; }
        public string CapabilityVersion { get; set; }
    }
}
