using gpconnect_user_portal.DTO.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Response = gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Response.SiteDefinition> GetSiteDefinition(string siteUniqueIdentifier);
        Task<Response.SiteDefinition> AddSiteDefinition(string siteDefinition);
        Task PostSiteDefinition(Guid siteUniqueIdentifier);
        Task<Task> AddSiteDefinitionsFromFeed(List<SiteDefinition> siteDefinitions);
        Task<List<Response.EndpointChange>> GetEndpointChanges(EndpointChange endpointChange);
        Task<List<Response.EndpointChangeCountByStatus>> GetEndpointChangeCountByStatus();
    }
}
