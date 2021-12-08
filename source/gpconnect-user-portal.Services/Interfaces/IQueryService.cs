using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IQueryService
    {
        Task<SearchResult> GetSites(SearchRequest searchRequest = null);
    }
}
