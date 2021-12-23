using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SiteAttributeMap : EntityMap<SiteAttribute>
    {
        public SiteAttributeMap()
        {
            Map(p => p.SiteAttributeId).ToColumn("site_attribute_id");
            Map(p => p.SiteAttributeName).ToColumn("site_attribute_name");
            Map(p => p.SiteAttributeValue).ToColumn("site_attribute_value");
            Map(p => p.LookupValue).ToColumn("lookup_value");
            Map(p => p.SiteUniqueIdentifier).ToColumn("site_unique_identifier");
            Map(p => p.SiteDefinitionId).ToColumn("site_definition_id");
        }
    }
}