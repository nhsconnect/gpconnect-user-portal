using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SearchResultEntryMap : EntityMap<SearchResultEntry>
    {
        public SearchResultEntryMap()
        {
            Map(p => p.SiteODSCode).ToColumn("SiteODS");
            Map(p => p.SiteName).ToColumn("SiteName");
            Map(p => p.CCGODSCode).ToColumn("CCGODS");
            Map(p => p.CCGName).ToColumn("CCGName");
            Map(p => p.Interactions).ToColumn("Interactions");
        }
    }
}
