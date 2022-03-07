using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Services.Enumerations;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IQueryService
    {
        Task<DTO.Response.Application.Search.SearchResult> GetSites(SiteDefinitionStatus siteDefinitionStatusMin, SiteDefinitionStatus siteDefinitionStatusMax, SearchRequest searchRequest = null);
    }
}
