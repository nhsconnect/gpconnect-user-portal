using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDataService _dataService;
        private readonly IEmailService _emailService;

        public ApplicationService(IDataService dataService, IEmailService emailService)
        {
            _dataService = dataService;
            _emailService = emailService;
        }

        public async Task<DTO.Response.Application.SiteDefinition> GetSiteDefinition(Guid siteUniqueIdentifier)
        {
            var query = "application.get_site_definition";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);

            var siteDefinition = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteDefinition>(query, parameters);

            if(siteDefinition != null)
            {
                siteDefinition.CanUpdateOrSubmit = siteDefinition.SiteDefinitionStatusId == (int)SiteDefinitionStatus.Draft;
                var siteAttributes = await GetSiteAttributes(siteDefinition.SiteUniqueIdentifier);
                siteDefinition.SiteAttributes = siteAttributes;
            }
            return siteDefinition;
        }

        public async Task<DTO.Response.Application.SiteDefinition> AddSiteDefinition(IFormCollection collection, SiteDefinition siteDefinition)
        {
            var existingSiteDefinition = await GetSiteDefinition(siteDefinition.SiteUniqueIdentifier);
            if(existingSiteDefinition != null)
            {
                return await UpdateSiteDefinition(collection, siteDefinition);
            }
            var parameters = new DynamicParameters();
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid, DbType.String, ParameterDirection.Input);
            return await AddOrUpdateSiteDefinition("application.add_site_definition", collection, "application.add_site_attribute", parameters);
        }

        public async Task PostSiteDefinition(Guid siteUniqueIdentifier)
        {            
            var parameters = new Dictionary<string, Guid>
            {
                { "_site_unique_identifier", siteUniqueIdentifier }
            };

            var emailDefinition = new EmailDefinition()
            {
                SiteUniqueIdentifier = siteUniqueIdentifier,
                SiteDefinition = _dataService.ExecuteQueryAndGetDataTable("application.get_site_definition_friendly", parameters),
                SiteAttributes = _dataService.ExecuteQueryAndGetDataTable("application.get_site_attributes_friendly", parameters)
            };

            await _emailService.SendSiteNotificationEmail(emailDefinition);
            await UpdateSiteDefinitionStatus(siteUniqueIdentifier, SiteDefinitionStatus.Submitted);
        }

        private async Task UpdateSiteDefinitionStatus(Guid siteUniqueIdentifier, SiteDefinitionStatus siteDefinitionStatus)
        {
            var query = "application.update_site_definition_status";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_definition_status_id", (int)siteDefinitionStatus, DbType.Int16, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        private async Task<DTO.Response.Application.SiteDefinition> UpdateSiteDefinition(IFormCollection collection, SiteDefinition siteDefinition)
        {
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteDefinition.SiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid, DbType.String, ParameterDirection.Input);
            return await AddOrUpdateSiteDefinition("application.update_site_definition", collection, "application.update_site_attribute", parameters);
        }

        private async Task<DTO.Response.Application.SiteDefinition> AddOrUpdateSiteDefinition(string siteDefinitionQuery, IFormCollection collection, string siteAttributeQuery, DynamicParameters parameters)
        {
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteDefinition>(siteDefinitionQuery, parameters);

            if (result != null)
            {
                var siteAttributes = new List<DTO.Request.SiteAttribute>();
                siteAttributes.AddRange(collection.Select(x => new DTO.Request.SiteAttribute { SiteAttributeName = x.Key, SiteAttributeValue = x.Value }).ToList());
                await AddOrUpdateSiteAttributes(siteAttributeQuery, result.SiteUniqueIdentifier, siteAttributes);
                return result;
            }
            return null;
        }

        private async Task AddOrUpdateSiteAttributes(string query, Guid siteUniqueIdentifer, List<SiteAttribute> siteAttributes)
        {
            foreach (var siteAttribute in siteAttributes)
            {
                var parameters = new DynamicParameters();
                parameters.Add("_site_unique_identifier", siteUniqueIdentifer, DbType.Guid, ParameterDirection.Input);
                parameters.Add("_site_attribute_name", siteAttribute.SiteAttributeName, DbType.String, ParameterDirection.Input);
                parameters.Add("_site_attribute_value", siteAttribute.SiteAttributeValue, DbType.String, ParameterDirection.Input);
                await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteAttribute>(query, parameters);
            }
        }

        private async Task<List<DTO.Response.Application.SiteAttribute>> GetSiteAttributes(Guid siteUniqueIdentifier)
        {
            var query = "application.get_site_attributes";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            var result = await _dataService.ExecuteQuery<DTO.Response.Application.SiteAttribute>(query, parameters);
            return result;
        }
    }
}
