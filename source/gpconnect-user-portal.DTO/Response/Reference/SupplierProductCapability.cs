namespace gpconnect_user_portal.DTO.Response.Reference
{
    public class SupplierProductCapability
    {
        public int SupplierProductCapabilityId { get; set; }
        public int SupplierProductId { get; set; }
        public int ProductCapabilityId { get; set; }
        public string LookupValue { get; set; }
        public int SupplierId { get; set; }
        public bool ProviderAssured { get; set; }
        public bool ConsumerAssured { get; set; }
    }
}
