using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class EndpointChangeMap : EntityMap<EndpointChange>
    {
        public EndpointChangeMap()
        {
            Map(p => p.SiteName).ToColumn("site_name");
            Map(p => p.SubmittedDate).ToColumn("submitted_date");
            Map(p => p.SiteDefinitionStatusName).ToColumn("site_definition_status_name");
            Map(p => p.SiteUniqueIdentifier).ToColumn("site_unique_identifier");
            Map(p => p.SiteDefinitionId).ToColumn("site_definition_id");
            Map(p => p.SiteDefinitionStatusId).ToColumn("site_definition_status_id");            
        }
    }
}