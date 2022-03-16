using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class EndpointChangeCountByStatusMap : EntityMap<EndpointChangeCountByStatus>
    {
        public EndpointChangeCountByStatusMap()
        {
            Map(p => p.SiteDefinitionStatusId).ToColumn("site_definition_status_id");
            Map(p => p.SiteDefinitionStatusName).ToColumn("site_definition_status_name");
            Map(p => p.SiteDefinitionStatusCount).ToColumn("site_definition_status_count");
        }
    }
}