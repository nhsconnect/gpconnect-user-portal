using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Response.Application;
using gpconnect_user_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDataService _dataService;

        public ApplicationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<SiteDefinition> GetSiteDefinition(Guid siteUniqueIdentifier)
        {
            var query = "application.get_site_definition";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            var siteDefinition = await _dataService.ExecuteQueryFirstOrDefault<SiteDefinition>(query, parameters);

            if(siteDefinition != null)
            {
                var siteAttributes = await GetSiteAttributes(siteDefinition.SiteUniqueIdentifier);
                siteDefinition.SiteAttributes = siteAttributes;
            }
            return siteDefinition;
        }

        public async Task<SiteDefinition> AddSiteDefinition(DTO.Request.SiteDefinition siteDefinition)
        {
            var query = "application.add_site_definition";
            var parameters = new DynamicParameters();
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid, DbType.String, ParameterDirection.Input);
            var createdSiteDefinition = await _dataService.ExecuteQueryFirstOrDefault<SiteDefinition>(query, parameters);

            if (createdSiteDefinition != null && siteDefinition.SiteAttributes?.Count > 0)
            {
                await AddSiteAttributes(createdSiteDefinition.SiteUniqueIdentifier, siteDefinition.SiteAttributes);                
                return createdSiteDefinition;
            }
            return null;            
        }

        private async Task AddSiteAttributes(Guid siteUniqueIdentifer, List<DTO.Request.SiteAttribute> siteAttributes)
        {
            var query = "application.add_site_attribute";
            foreach (var siteAttribute in siteAttributes)
            {
                var parameters = new DynamicParameters();
                parameters.Add("_site_unique_identifier", siteUniqueIdentifer, DbType.Guid, ParameterDirection.Input);
                parameters.Add("_site_attribute_name", siteAttribute.SiteAttributeName, DbType.String, ParameterDirection.Input);
                parameters.Add("_site_attribute_value", siteAttribute.SiteAttributeValue, DbType.String, ParameterDirection.Input);
                await _dataService.ExecuteQueryFirstOrDefault<SiteAttribute>(query, parameters);
            }
        }

        private async Task<List<SiteAttribute>> GetSiteAttributes(Guid siteUniqueIdentifier)
        {
            var query = "application.get_site_attributes";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            var result = await _dataService.ExecuteQuery<SiteAttribute>(query, parameters);
            return result;
        }
    }
}
