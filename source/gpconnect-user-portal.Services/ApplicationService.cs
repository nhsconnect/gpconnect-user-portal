using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Request.Registration;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
        private readonly IHttpContextAccessor _context;

        public ApplicationService(IDataService dataService, IEmailService emailService, IHttpContextAccessor context)
        {
            _dataService = dataService;
            _emailService = emailService;
            _context = context;
        }

        public Task<Task> AddSiteDefinitionsFromFeed(List<SiteDefinition> siteDefinitions)
        {
            siteDefinitions.ForEach(async siteDefinition =>
            {
                var masterSiteDefinition = await AddSiteDefinitionAndAttributesFromFeed(siteDefinition, SiteDefinitionStatus.Live);
                
                siteDefinition.SiteUniqueIdentifier = Guid.NewGuid();
                siteDefinition.MasterSiteUniqueIdentifier = masterSiteDefinition.SiteUniqueIdentifier;
                await AddSiteDefinitionAndAttributesFromFeed(siteDefinition, SiteDefinitionStatus.Completed);
            });

            return Task.FromResult(Task.CompletedTask);
        }

        public async Task<List<DTO.Response.Application.EndpointChangeCountByStatus>> GetEndpointChangeCountByStatus()
        {
            var query = "application.get_endpoint_change_count_by_status";
            var result = await _dataService.ExecuteQuery<DTO.Response.Application.EndpointChangeCountByStatus>(query);
            return result;
        }

        public async Task<List<DTO.Response.Application.EndpointChange>> GetEndpointChanges(EndpointChange endpointChange)
        {
            var query = "application.get_endpoint_changes";
            var parameters = new DynamicParameters();
            parameters.Add("_site_definition_status_id_lower_band", endpointChange.SiteDefinitionStatusIdLowerBand, DbType.Int16, ParameterDirection.Input);

            if (!endpointChange.SiteDefinitionStatusIdUpperBand.HasValue)
            {
                parameters.Add("_site_definition_status_id_upper_band", endpointChange.SiteDefinitionStatusIdLowerBand, DbType.Int16, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("_site_definition_status_id_upper_band", endpointChange.SiteDefinitionStatusIdUpperBand.Value, DbType.Int16, ParameterDirection.Input);
            }

            if (endpointChange.SearchDateFrom != null)
            {
                parameters.Add("_search_date_from", endpointChange.SearchDateFrom, DbType.DateTime, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("_search_date_from", DateTime.MinValue, DbType.DateTime, ParameterDirection.Input);
            }

            if (endpointChange.SearchDateTo != null)
            {
                parameters.Add("_search_date_to", endpointChange.SearchDateFrom, DbType.DateTime, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("_search_date_to", DateTime.MaxValue, DbType.DateTime, ParameterDirection.Input);
            }

            if (endpointChange.SearchValue != null)
            {
                parameters.Add("_search_value", endpointChange.SearchValue.SearchAndReplace(new Dictionary<string, string> { { ",", "|" }, { " ", "|" } }).Replace("||", "|"), DbType.String, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("_search_value", null, DbType.String, ParameterDirection.Input);
            }

            var result = await _dataService.ExecuteQuery<DTO.Response.Application.EndpointChange>(query, parameters);
            return result;
        }

        private async Task<DTO.Response.Application.SiteDefinition> AddSiteDefinitionAndAttributesFromFeed(SiteDefinition siteDefinition, SiteDefinitionStatus siteDefinitionStatus)
        {
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteDefinition.SiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid, DbType.String, ParameterDirection.Input);            
            parameters.Add("_site_definition_status", (int)siteDefinitionStatus, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_site_interactions", siteDefinition.SiteInteractions, DbType.String, ParameterDirection.Input);

            if(siteDefinition.MasterSiteUniqueIdentifier.HasValue)
            {
                parameters.Add("_master_site_unique_identifier", siteDefinition.MasterSiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("_master_site_unique_identifier", null, DbType.Guid, ParameterDirection.Input);
            }

            var insertedSiteDefinition = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteDefinition>("application.add_site_definition", parameters);

            foreach (var siteAttribute in siteDefinition.SiteAttribute)
            {
                parameters = new DynamicParameters();
                parameters.Add("_site_unique_identifier", insertedSiteDefinition.SiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
                parameters.Add("_site_attribute_name", siteAttribute.SiteAttributeName, DbType.String, ParameterDirection.Input);
                parameters.Add("_site_attribute_value", siteAttribute.SiteAttributeValue, DbType.String, ParameterDirection.Input);
                await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteAttribute>("application.add_site_attribute", parameters);
            }
            return insertedSiteDefinition;
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

        public async Task<DTO.Response.Application.SiteDefinition> GetSiteDefinition(string siteUniqueIdentifier)
        {
            var query = "application.get_site_definition";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", Guid.Parse(siteUniqueIdentifier), DbType.Guid, ParameterDirection.Input);

            var siteDefinition = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteDefinition>(query, parameters);

            if (siteDefinition != null)
            {
                siteDefinition.CanUpdateOrSubmit = siteDefinition.SiteDefinitionStatusId == (int)SiteDefinitionStatus.Draft;
                var siteAttributes = await GetSiteAttributes(siteDefinition.SiteUniqueIdentifier);
                siteDefinition.SiteAttributes = siteAttributes;
            }
            return siteDefinition;
        }

        public async Task<DTO.Response.Application.SiteDefinition> AddSiteDefinition(string siteDefinitionRegistrationData)
        {
            var siteDefinitionRegistration = JsonConvert.DeserializeObject<SiteDefinitionRegistration>(siteDefinitionRegistrationData);
            var siteDefinition = MapSiteDefinitionFromSiteDefinitionRegistration(siteDefinitionRegistration);

            var existingSiteDefinition = await GetSiteDefinition(siteDefinition.SiteUniqueIdentifier.ToString());
            if (existingSiteDefinition != null)
            {
                return await UpdateSiteDefinition(siteDefinition);
            }

            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteDefinition.SiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_definition_status", (int)SiteDefinitionStatus.Draft, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_site_interactions", siteDefinition.SiteInteractions, DbType.String, ParameterDirection.Input);

            return await AddOrUpdateSiteDefinition("application.add_site_definition", parameters, "application.add_site_attribute", siteDefinition);
        }

        private SiteDefinition MapSiteDefinitionFromSiteDefinitionRegistration(SiteDefinitionRegistration siteDefinitionRegistration)
        {
            var siteDefinition = new SiteDefinition()
            {
                SiteOdsCode = siteDefinitionRegistration.EndpointSiteDetails.OdsCode
            };

            if (!string.IsNullOrEmpty(siteDefinitionRegistration.SiteUniqueIdentifier))
            {
                siteDefinition.SiteUniqueIdentifier = Guid.Parse(siteDefinitionRegistration.SiteUniqueIdentifier);
            }
            else
            {
                siteDefinition.SiteUniqueIdentifier = Guid.NewGuid();
            }

            var siteAttributes = new List<SiteAttribute>();
            
            foreach(var entry in DictionaryExtensions.ToDictionary<string>(siteDefinitionRegistration.EndpointSiteDetails))
            {
                siteAttributes.Add(new SiteAttribute { SiteAttributeName = entry.Key, SiteAttributeValue = entry.Value });
            }
            foreach (var entry in DictionaryExtensions.ToDictionary<string>(siteDefinitionRegistration.EndpointSubmitterDetails))
            {
                siteAttributes.Add(new SiteAttribute { SiteAttributeName = entry.Key, SiteAttributeValue = entry.Value });
            }
            foreach (var entry in DictionaryExtensions.ToDictionary<string>(siteDefinitionRegistration.EndpointDataSharingAgreementContactDetails))
            {
                siteAttributes.Add(new SiteAttribute { SiteAttributeName = entry.Key, SiteAttributeValue = entry.Value });
            }
            foreach (var entry in DictionaryExtensions.ToDictionary<string>(siteDefinitionRegistration.EndpointSupplierDetails))
            {
                siteAttributes.Add(new SiteAttribute { SiteAttributeName = entry.Key, SiteAttributeValue = entry.Value });
            }
            foreach (var entry in DictionaryExtensions.ToDictionary<string>(siteDefinitionRegistration.EndpointSupplierProductCapability))
            {
                siteAttributes.Add(new SiteAttribute { SiteAttributeName = entry.Key, SiteAttributeValue = entry.Value });
            }

            siteDefinition.SiteAttribute = siteAttributes;
            return siteDefinition;
        }

        public async Task PostSiteDefinition(string siteUniqueIdentifier)
        {
            var siteGuid = Guid.Parse(siteUniqueIdentifier);
            var parameters = new Dictionary<string, Guid>
            {
                { "_site_unique_identifier", siteGuid }
            };

            var siteDefinition = await GetSiteDefinition(siteUniqueIdentifier);

            var emailDefinition = new EmailDefinition()
            {
                SiteUniqueIdentifier = siteGuid,
                SiteDefinition = _dataService.ExecuteQueryAndGetDataTable("application.get_site_definition_friendly", parameters, true),
                SiteAttributes = _dataService.ExecuteQueryAndGetDataTable("application.get_site_attributes_friendly", parameters, true)
            };

            await _emailService.SendSiteNotificationEmail(siteDefinition.SiteDefinitionStatusId, emailDefinition);
            await UpdateSiteDefinitionStatus(siteGuid, SiteDefinitionStatus.AwaitingReview);
        }

        private async Task UpdateSiteDefinitionStatus(Guid siteUniqueIdentifier, SiteDefinitionStatus siteDefinitionStatus)
        {
            var query = "application.update_site_definition_status";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_definition_status_id", (int)siteDefinitionStatus, DbType.Int16, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        private async Task<DTO.Response.Application.SiteDefinition> UpdateSiteDefinition(SiteDefinition siteDefinition)
        {
            await UpdateSiteDefinitionStatus(siteDefinition.SiteUniqueIdentifier, SiteDefinitionStatus.Draft);
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteDefinition.SiteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            parameters.Add("_site_ods_code", siteDefinition.SiteOdsCode ?? null, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_party_key", siteDefinition.SitePartyKey ?? null, DbType.String, ParameterDirection.Input);
            parameters.Add("_site_asid", siteDefinition.SiteAsid ?? null, DbType.String, ParameterDirection.Input);

            return await AddOrUpdateSiteDefinition("application.update_site_definition", parameters, "application.add_site_attribute", siteDefinition);
        }

        private async Task<DTO.Response.Application.SiteDefinition> AddOrUpdateSiteDefinition(string siteDefinitionQuery, DynamicParameters parameters, string siteAttributeQuery, SiteDefinition siteDefinition)
        {
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.SiteDefinition>(siteDefinitionQuery, parameters);
            if (result != null)
            {
                await AddOrUpdateSiteAttributes(siteAttributeQuery, result.SiteUniqueIdentifier, siteDefinition.SiteAttribute);
                return result;
            }
            return null;
        }

        private async Task<List<DTO.Response.Application.SiteAttribute>> GetSiteAttributes(Guid siteUniqueIdentifier)
        {
            var query = "application.get_site_attributes";
            var parameters = new DynamicParameters();
            parameters.Add("_site_unique_identifier", siteUniqueIdentifier, DbType.Guid, ParameterDirection.Input);
            var result = await _dataService.ExecuteQuery<DTO.Response.Application.SiteAttribute>(query, parameters);
            return result;
        }

        public async Task<List<DTO.Response.Application.User>> GetUsers()
        {
            var query = "application.get_users";
            var result = await _dataService.ExecuteQuery<DTO.Response.Application.User>(query);
            return result;
        }

        public async Task SetIsAdmin(int userId, bool isAdmin)
        {
            var query = "application.set_user_isadmin";
            var parameters = new DynamicParameters();
            parameters.Add("_admin_user_id", _context.HttpContext?.User?.GetClaimValue("UserId", nullIfEmpty: true).StringToInteger(), DbType.Int16, ParameterDirection.Input);
            parameters.Add("_user_id", userId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_is_admin", isAdmin, DbType.Boolean, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        public async Task<DTO.Response.Application.User> GetUser(string emailAddress)
        {
            var query = "application.get_user";
            var parameters = new DynamicParameters();
            parameters.Add("_email_address", emailAddress, DbType.String, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.User>(query, parameters);
            return result;
        }

        public async Task<DTO.Response.Application.User> LogonUser(DTO.Request.Application.User user)
        {
            var query = "application.logon_user";
            var parameters = new DynamicParameters();
            parameters.Add("_email_address", user.EmailAddress, DbType.String, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.User>(query, parameters);
            return result;
        }

        public async Task<DTO.Response.Application.User> LogoffUser(DTO.Request.Application.User user)
        {
            var query = "application.logoff_user";
            var parameters = new DynamicParameters();
            parameters.Add("_email_address", user.EmailAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("_user_session_id", user.UserSessionId, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Application.User>(query, parameters);
            return result;
        }
    }
}