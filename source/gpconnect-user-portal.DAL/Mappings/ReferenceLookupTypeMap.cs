using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceLookupTypeMap : EntityMap<LookupType>
    {
        public ReferenceLookupTypeMap()
        {
            Map(p => p.LookupTypeId).ToColumn("lookup_type_id");
            Map(p => p.LookupTypeName).ToColumn("lookup_type_name");
        }
    }
}
