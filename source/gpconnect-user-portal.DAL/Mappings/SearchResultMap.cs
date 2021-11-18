using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SearchResultMap : EntityMap<SearchResult>
    {
        public SearchResultMap()
        {
            Map(p => p.SearchResultId).ToColumn("SearchResultId");
        }
    }
}
