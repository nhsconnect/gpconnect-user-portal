using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Configuration;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceApiQueryMap : EntityMap<ReferenceApiQuery>
    {
        public ReferenceApiQueryMap()
        {
            Map(p => p.QueryName).ToColumn("query_name");
            Map(p => p.QueryText).ToColumn("query_text");
        }
    }
}
