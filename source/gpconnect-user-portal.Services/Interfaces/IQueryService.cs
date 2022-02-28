using gpconnect_user_portal.DTO.Request;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IQueryService
    {
        Task<DTO.Response.Application.Search.SearchResult> GetSites(SearchRequest searchRequest = null);
    }
}
