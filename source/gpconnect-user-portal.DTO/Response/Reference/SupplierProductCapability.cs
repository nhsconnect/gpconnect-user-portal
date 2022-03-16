using gpconnect_user_portal.Resources;
using System;
using System.Resources;

namespace gpconnect_user_portal.DTO.Response.Reference
{
    public class SupplierProductCapability
    {
        private readonly ResourceManager _resourceManager;

        public SupplierProductCapability()
        {
            _resourceManager = new ResourceManager("gpconnect_user_portal.Resources.DataFieldNameResources", typeof(DataFieldNameResources).Assembly);
        }

        public int SupplierProductCapabilityId { get; set; }
        public int SupplierProductId { get; set; }
        public int ProductCapabilityId { get; set; }
        public string LookupValue { get; set; }
        public string LookupValueAssurance => _resourceManager.GetString(LookupValue + "Assurance");
        public int SupplierId { get; set; }
        public bool ProviderAssured { get; set; }
        public bool ConsumerAssured { get; set; }
        public bool AwaitingAssurance { get; set; }
        public DateTime? AssuranceDate { get; set; }
        public string CapabilityVersion { get; set; }
    }
}
