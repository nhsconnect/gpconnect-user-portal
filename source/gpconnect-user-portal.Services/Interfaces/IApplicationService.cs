using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Request = gpconnect_user_portal.DTO.Request;
using Response = gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Response.SiteDefinition> GetSiteDefinition(Guid siteUniqueIdentifier);
        Task<Response.SiteDefinition> AddSiteDefinition(IFormCollection formCollection, Request.SiteDefinition siteDefinition);
        Task PostSiteDefinition(Guid siteUniqueIdentifier);
    }
}
