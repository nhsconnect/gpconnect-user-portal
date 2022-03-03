using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceLookupMap : EntityMap<Lookup>
    {
        public ReferenceLookupMap()
        {
            Map(p => p.LookupId).ToColumn("lookup_id");
            Map(p => p.LookupTypeId).ToColumn("lookup_type_id");
            Map(p => p.LookupValue).ToColumn("lookup_value");
            Map(p => p.LookupTypeName).ToColumn("lookup_type_name");
            Map(p => p.LookupTypeDescription).ToColumn("lookup_type_description");
            Map(p => p.LinkedLookupId).ToColumn("linked_lookup_id");
            Map(p => p.LinkedLookupValue).ToColumn("linked_lookup_value");
            Map(p => p.IsDisabled).ToColumn("is_disabled");            
        }
    }
}
