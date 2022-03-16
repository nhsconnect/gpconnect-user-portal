using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SiteDefinitionMap : EntityMap<SiteDefinition>
    {
        public SiteDefinitionMap()
        {
            Map(p => p.SiteAsid).ToColumn("site_asid");
            Map(p => p.SitePartyKey).ToColumn("site_party_key");
            Map(p => p.SiteOdsCode).ToColumn("site_ods_code");
            Map(p => p.SiteUniqueIdentifier).ToColumn("site_unique_identifier");
            Map(p => p.MasterSiteUniqueIdentifier).ToColumn("master_site_unique_identifier");
            Map(p => p.SiteDefinitionId).ToColumn("site_definition_id");
            Map(p => p.SiteDefinitionStatusId).ToColumn("site_definition_status_id");            
            Map(p => p.SubmittedDate).ToColumn("submitted_date");
            Map(p => p.SiteDefinitionStatusName).ToColumn("site_definition_status_name");
        }
    }
}