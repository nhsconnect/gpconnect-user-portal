using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SupplierProductCapabilityMap : EntityMap<SupplierProductCapability>
    {
        public SupplierProductCapabilityMap()
        {
            Map(p => p.SupplierProductCapabilityId).ToColumn("supplier_product_capability_id");
            Map(p => p.SupplierProductId).ToColumn("supplier_product_id");
            Map(p => p.ProductCapabilityId).ToColumn("product_capability_id");
            Map(p => p.LookupValue).ToColumn("lookup_value");
            Map(p => p.SupplierId).ToColumn("supplier_id");
            Map(p => p.ProviderAssured).ToColumn("provider_assured");
            Map(p => p.ConsumerAssured).ToColumn("consumer_assured");
            Map(p => p.AwaitingAssurance).ToColumn("awaiting_assurance");
            Map(p => p.AssuranceDate).ToColumn("assurance_date");
            Map(p => p.CapabilityVersion).ToColumn("capability_version");
        }
    }
}