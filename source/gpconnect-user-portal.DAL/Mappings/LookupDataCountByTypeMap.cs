using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class LookupDataCountByTypeMap : EntityMap<LookupDataCountByType>
    {
        public LookupDataCountByTypeMap()
        {
            Map(p => p.LookupTypeId).ToColumn("lookup_type_id");
            Map(p => p.LookupTypeName).ToColumn("lookup_type_name");
            Map(p => p.LookupTypeDescription).ToColumn("lookup_type_description");
            Map(p => p.LookupTypeCount).ToColumn("lookup_type_count");
        }
    }
}