using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application.Search;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SearchResultEntryMap : EntityMap<SearchResultEntry>
    {
        public SearchResultEntryMap()
        {
            Map(p => p.SiteDefinitionId).ToColumn("site_definition_id");
            Map(p => p.SiteODSCode).ToColumn("site_ods_code");
            Map(p => p.SiteUniqueIdentifier).ToColumn("site_unique_identifier");
            Map(p => p.SiteDefinitionStatusId).ToColumn("site_definition_status_id");
            Map(p => p.SiteInteractions).ToColumn("site_interactions");
            Map(p => p.SiteAttributesArray).ToColumn("site_attributes_array");
        }
    }
}
