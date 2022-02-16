using Dapper;
using gpconnect_user_portal.DAL.Enumerations;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response.Fhir;
using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IFhirRequestExecution _fhirRequestExecution;
        private readonly IDataService _dataService;
        private readonly IConfigurationService _configurationService;

        public ReferenceService(IFhirRequestExecution fhirRequestExecution, IConfigurationService configurationService, IDataService dataService)
        {
            _fhirRequestExecution = fhirRequestExecution;
            _configurationService = configurationService;
            _dataService = dataService;
        }

        public async Task<List<DTO.Response.Reference.Organisation>> GetOrganisations()
        {
            var query = "reference.get_organisations";
            var result = await _dataService.ExecuteQuery<DTO.Response.Reference.Organisation>(query);
            return result;
        }

        public async Task<List<LookupDataCountByType>> GetLookupDataCountByType()
        {
            var query = "reference.get_lookup_data_count_by_type";
            var result = await _dataService.ExecuteQuery<LookupDataCountByType>(query);
            return result;
        }

        public async Task<DTO.Response.Reference.Organisation> GetOrganisation(string odsCode)
        {
            var query = "reference.get_organisation";
            var parameters = new DynamicParameters();
            parameters.Add("_ods_code", odsCode, DbType.String, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<DTO.Response.Reference.Organisation>(query, parameters);
            return result;
        }

        public async Task<List<SiteDefinition>> GetSiteDefinitions()
        {
            try
            {
                var organisationsWithInteractions = await _configurationService.GetFhirApiQuery(FhirApiQueryTypes.GetOrganisationsWithInteractions);
                var organisationsDetails = await _configurationService.GetFhirApiQuery(FhirApiQueryTypes.GetOrganisationDetails);

                var spineConfiguration = await _configurationService.GetSpineConfiguration();
                if (organisationsWithInteractions != null && spineConfiguration != null)
                {
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    var response = await _fhirRequestExecution.ExecuteFhirQuery<OrganisationsWithInteractions>(organisationsWithInteractions.QueryText, token, spineConfiguration.SpineFhirApiSystemsRegisterFqdn, spineConfiguration.SpineFhirApiKey);
                    var tasks = new ConcurrentBag<Task<SiteDefinition>>();

                    Parallel.ForEach(response.SiteDefinitions, siteDefinition =>
                    {
                        tasks.Add(UpdateSiteWithOrganisationDetails(spineConfiguration, token, organisationsDetails, siteDefinition));
                    });
                    var results = await Task.WhenAll(tasks);
                    return results.ToList();
                }
            }
            catch
            {
                throw;             
            }
            return null;
        }

        private async Task<SiteDefinition> UpdateSiteWithOrganisationDetails(DTO.Response.Configuration.Spine spineConfiguration, CancellationToken cancellationToken, DTO.Response.Configuration.FhirApiQuery fhirApiQuery, SiteDefinition siteDefinition)
        {
            var organisationQuery = fhirApiQuery.QueryText.SearchAndReplace(new Dictionary<string, string> { { "{odsCode}", Regex.Escape(siteDefinition.SiteOdsCode) } });
            var supplierQuery = fhirApiQuery.QueryText.SearchAndReplace(new Dictionary<string, string> { { "{odsCode}", Regex.Escape(siteDefinition.SupplierOdsCode) } });
            var organisationDetail = await _fhirRequestExecution.ExecuteFhirQuery<OrganisationDetail>(organisationQuery, cancellationToken, spineConfiguration.SpineFhirApiDirectoryServicesFqdn, spineConfiguration.SpineFhirApiKey);
            var supplierDetail = await _fhirRequestExecution.ExecuteFhirQuery<OrganisationDetail>(supplierQuery, cancellationToken, spineConfiguration.SpineFhirApiDirectoryServicesFqdn, spineConfiguration.SpineFhirApiKey);

            siteDefinition.SiteAttribute = new List<SiteAttribute>
            {
                new SiteAttribute() { SiteAttributeName = "SiteName", SiteAttributeValue = organisationDetail?.SiteName },
                new SiteAttribute() { SiteAttributeName = "ContactTelephone", SiteAttributeValue = organisationDetail?.TelephoneNumber },
                new SiteAttribute() { SiteAttributeName = "SitePostcode", SiteAttributeValue = organisationDetail?.PostCode },
                new SiteAttribute() { SiteAttributeName = "SelectedCCGOdsCode", SiteAttributeValue = organisationDetail?.CCGOdsCode },
                new SiteAttribute() { SiteAttributeName = "SelectedCCGName", SiteAttributeValue = (await GetOrganisation(organisationDetail?.CCGOdsCode))?.Name },
                new SiteAttribute() { SiteAttributeName = "FormOdsCode", SiteAttributeValue = siteDefinition.SiteOdsCode },
                new SiteAttribute() { SiteAttributeName = "SelectedSupplier", SiteAttributeValue = supplierDetail?.Organisation?.Name }
            };
            return siteDefinition;
        }

        public async Task<Task> GetCCGs()
        {
            try
            {
                var query = await _configurationService.GetReferenceApiQuery(ReferenceApiQueryTypes.GetActiveCCGOrganisationsFromSDS);
                if (query != null)
                {
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    var response = await _fhirRequestExecution.ExecuteFhirQuery<CCG>(query.QueryText, token);
                    if (response != null)
                    {
                        await SynchroniseOrganisations(response.Organisations);
                    }
                }
                return Task.CompletedTask;
            }
            catch(Exception exc)
            {
                return Task.FromException(exc);
            }
        }

        private async Task SynchroniseOrganisations(List<DTO.Response.Reference.Organisation> organisations)
        {
            var query = "reference.synchronise_organisation";
            var parameters = new DynamicParameters();
            foreach (var organisation in organisations)
            {
                parameters.Add("_ods_code", organisation.OdsCode, DbType.String, ParameterDirection.Input);
                parameters.Add("_organisation_name", organisation.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("_org_status", organisation.Status, DbType.String, ParameterDirection.Input);
                parameters.Add("_org_record_class", organisation.OrgRecordClass, DbType.String, ParameterDirection.Input);
                parameters.Add("_last_change_date", organisation.LastChangeDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("_primary_role_description", organisation.PrimaryRoleDescription, DbType.String, ParameterDirection.Input);
                parameters.Add("_primary_role_id", organisation.PrimaryRoleId, DbType.String, ParameterDirection.Input);
                parameters.Add("_organisation_link", organisation.OrgLink, DbType.String, ParameterDirection.Input);
                parameters.Add("_postcode", organisation.Postcode, DbType.String, ParameterDirection.Input);
                await _dataService.ExecuteQuery(query, parameters);
            }
        }

        public async Task<List<LookupType>> GetLookupTypes()
        {
            var query = "reference.get_lookup_types";
            var result = await _dataService.ExecuteQuery<LookupType>(query);
            return result;
        }

        public async Task<List<Lookup>> GetLookups()
        {
            var query = "reference.get_lookups";
            var result = await _dataService.ExecuteQuery<Lookup>(query);
            return result;
        }

        public async Task<List<Lookup>> GetLookup(Enumerations.LookupType lookupTypeId)
        {
            var query = "reference.get_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_type_id", (int)lookupTypeId, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQuery<Lookup>(query, parameters);
            return result;
        }

        public async Task AddLookup(Enumerations.LookupType lookupTypeId, string lookupValue)
        {
            var query = "reference.get_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_type_id", (int)lookupTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_lookup_value", lookupValue, DbType.String, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        public async Task DisableLookup(int lookupId, DateTime? disableDate)
        {
            var query = "reference.disable_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_id", lookupId, DbType.Int16, ParameterDirection.Input);
            if (disableDate != null)
            {
                parameters.Add("_disable_date", disableDate, DbType.DateTime, ParameterDirection.Input);
            }
            await _dataService.ExecuteQuery(query, parameters);
        }

        public async Task<List<Lookup>> GetProductListWithSupplier()
        {
            var query = "reference.get_product_list_with_supplier";
            var result = await _dataService.ExecuteQuery<Lookup>(query);
            return result;
        }

        public async Task<EnabledSupplierProductCapability> GetSupplierProductCapabilities(int supplierProductId)
        {
            var query = "reference.get_supplier_product_capabilities";
            var parameters = new DynamicParameters();
            parameters.Add("_supplier_product_id", supplierProductId, DbType.Int16, ParameterDirection.Input);
            var supplierProductCapabilities = await _dataService.ExecuteQuery<SupplierProductCapability>(query, parameters);
            var enabledSupplierProductCapability = new EnabledSupplierProductCapability()
            {
                SupplierProductCapability = supplierProductCapabilities
            };
            return enabledSupplierProductCapability;
        }
    }
}
