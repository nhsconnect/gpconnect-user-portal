using gpconnect_user_portal.DTO.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using Request = gpconnect_user_portal.DTO.Request.Application;
using Response = gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Response.SiteDefinition> GetSiteDefinition(string siteUniqueIdentifier);
        Task<List<Response.EndpointChange>> GetEndpointChanges(EndpointChange endpointChange);
        Task<Response.SiteDefinition> AddSiteDefinition(string siteDefinition);
        Task PostSiteDefinition(string siteUniqueIdentifier);
        Task<Task> AddSiteDefinitionsFromFeed(List<SiteDefinition> siteDefinitions);
        Task<List<Response.EndpointChangeCountByStatus>> GetEndpointChangeCountByStatus();
        Task<List<Response.User>> GetUsers();
        Task<Response.User> LogonUser(Request.User user);
        Task<Response.User> LogoffUser(Request.User user);
        Task SetIsAdmin(int userId, bool isAdmin);
    }
}
