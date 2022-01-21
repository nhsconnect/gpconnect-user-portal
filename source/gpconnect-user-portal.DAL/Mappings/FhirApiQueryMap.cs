using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Configuration;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class FhirApiQueryMap : EntityMap<FhirApiQuery>
    {
        public FhirApiQueryMap()
        {
            Map(p => p.QueryName).ToColumn("query_name");
            Map(p => p.QueryText).ToColumn("query_text");
        }
    }
}
