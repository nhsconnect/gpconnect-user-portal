﻿using Dapper;
using gpconnect_user_portal.DAL.Enumerations;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response.Fhir;
using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Newtonsoft.Json;
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
        private readonly List<Lookup> _ccgNameList;
        private readonly List<Lookup> _ccgOdsCodeList;
        private readonly List<Lookup> _supplierList;

        public ReferenceService(IFhirRequestExecution fhirRequestExecution, IConfigurationService configurationService, IDataService dataService)
        {
            _fhirRequestExecution = fhirRequestExecution;
            _configurationService = configurationService;
            _dataService = dataService;
            _ccgOdsCodeList = GetLookup((int)Enumerations.LookupType.CCGICBODSCode).Result;
            _supplierList = GetLookup((int)Enumerations.LookupType.Supplier).Result;
        }

        public async Task<List<LookupDataCountByType>> GetLookupDataCountByType()
        {
            var query = "reference.get_lookup_data_count_by_type";
            var result = await _dataService.ExecuteQuery<LookupDataCountByType>(query);
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
                    var response = await _fhirRequestExecution.ExecuteFhirQuery<OrganisationsWithInteractions>(
                        organisationsWithInteractions.QueryText,
                        token,
                        spineConfiguration.SpineFhirApiSystemsRegisterFqdn,
                        spineConfiguration.SpineFhirApiKey
                    );
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
                new SiteAttribute() { SiteAttributeName = "SitePostcode", SiteAttributeValue = organisationDetail?.PostCode },
                new SiteAttribute() { SiteAttributeName = "SelectedCCGOdsCode", SiteAttributeValue = _ccgOdsCodeList.FirstOrDefault(x => x.LookupValue == organisationDetail?.CCGOdsCode)?.LookupId.ToString() },
                new SiteAttribute() { SiteAttributeName = "SelectedCCGName", SiteAttributeValue = _ccgOdsCodeList.FirstOrDefault(x => x.LookupValue == organisationDetail?.CCGOdsCode)?.LinkedLookupId.ToString() },
                new SiteAttribute() { SiteAttributeName = "OdsCode", SiteAttributeValue = siteDefinition.SiteOdsCode },
                new SiteAttribute() { SiteAttributeName = "SelectedSupplier", SiteAttributeValue = _supplierList.FirstOrDefault(x => x.LookupValue == supplierDetail?.Organisation?.Name)?.LookupId.ToString() },
                new SiteAttribute() { SiteAttributeName = "IsStructuredEnabled", SiteAttributeValue = siteDefinition.IsStructuredEnabled.ToString() },
                new SiteAttribute() { SiteAttributeName = "IsAppointmentEnabled", SiteAttributeValue = siteDefinition.IsAppointmentEnabled.ToString() },
                new SiteAttribute() { SiteAttributeName = "IsHtmlEnabled", SiteAttributeValue = siteDefinition.IsHtmlEnabled.ToString() },
                new SiteAttribute() { SiteAttributeName = "IsSendDocumentEnabled", SiteAttributeValue = siteDefinition.IsSendDocumentEnabled.ToString() }
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
                    var response = await _fhirRequestExecution.ExecuteFhirQuery<CCGs>(query.QueryText, token);
                    if (response != null)
                    {
                        await SynchroniseCCGs(response.CCG);
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception exc)
            {
                return Task.FromException(exc);
            }
        }

        private async Task SynchroniseCCGs(List<CCGDetail> CCGs)
        {
            var query = "reference.synchronise_ccgs";
            var parameters = new DynamicParameters();
            foreach (var ccg in CCGs)
            {
                parameters.Add("_ods_code", ccg.OdsCode, DbType.String, ParameterDirection.Input);
                parameters.Add("_organisation_name", ccg.OrganisationName, DbType.String, ParameterDirection.Input);
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

        public async Task<List<Lookup>> GetLookup(int lookupTypeId)
        {
            if (lookupTypeId > 0)
            {
                var query = "reference.get_lookup";
                var parameters = new DynamicParameters();
                parameters.Add("_lookup_type_id", lookupTypeId, DbType.Int16, ParameterDirection.Input);
                var result = await _dataService.ExecuteQuery<Lookup>(query, parameters);
                return result;
            }
            return null;
        }

        public async Task AddSupplier(string lookupValue)
        {
            await AddLookup(Enumerations.LookupType.Supplier, lookupValue);
        }

        public async Task AddProduct(DTO.Request.Reference.SupplierProduct supplierProduct)
        {
            var productAdded = await AddLookup(Enumerations.LookupType.SupplierProduct, supplierProduct.ProductName);
            if (productAdded != null)
            {
                await AddSupplierProduct(supplierProduct.SupplierId, productAdded.LookupId, supplierProduct.ProductUseCase);
            }
        }

        public async Task AddLookup(DTO.Request.Reference.Lookup lookup)
        {
            Enumerations.LookupType lookupTypeEnum = (Enumerations.LookupType)lookup.LookupTypeId;
            await AddLookup(lookupTypeEnum, lookup.LookupValue);
        }

        private async Task AddSupplierProduct(int supplierId, int supplierProductId, string productUseCase)
        {
            var query = "reference.add_supplier_product";
            var parameters = new DynamicParameters();
            parameters.Add("_supplier_id", supplierId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_supplier_product_id", supplierProductId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_product_use_case", productUseCase, DbType.String, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        private async Task<Lookup> AddLookup(Enumerations.LookupType lookupType, string lookupValue)
        {
            var query = "reference.add_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_type_id", (int)lookupType, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_lookup_value", lookupValue, DbType.String, ParameterDirection.Input);
            parameters.Add("_linked_lookup_id", null, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<Lookup>(query, parameters);
            return result;
        }

        public async Task EnableDisableLookup(int lookupId, bool isDisabled = false)
        {
            var query = "reference.enable_disable_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_id", lookupId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_is_disabled", isDisabled, DbType.Boolean, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        public async Task<List<Lookup>> GetProductListWithSupplier()
        {
            var query = "reference.get_product_list_with_supplier";
            var result = await _dataService.ExecuteQuery<Lookup>(query);
            return result;
        }

        public async Task<List<SupplierProducts>> GetSupplierProducts(int supplierId)
        {
            var query = "reference.get_supplier_products";
            var parameters = new DynamicParameters();
            parameters.Add("_supplier_id", supplierId, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQuery<SupplierProducts>(query, parameters);
            return result;
        }

        public async Task<EnabledSupplierProductCapability> GetSupplierProductCapabilities(int supplierProductId, bool includeNotEnabled = false)
        {
            var query = "reference.get_supplier_product_capabilities";
            var parameters = new DynamicParameters();
            parameters.Add("_supplier_product_id", supplierProductId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_include_supplier_product_capabilities_not_enabled", includeNotEnabled, DbType.Boolean, ParameterDirection.Input);
            var supplierProductCapabilities = await _dataService.ExecuteQuery<SupplierProductCapability>(query, parameters);
            var enabledSupplierProductCapability = new EnabledSupplierProductCapability()
            {
                SupplierProductCapability = supplierProductCapabilities
            };
            return enabledSupplierProductCapability;
        }

        public async Task UpdateLookup(int lookupId, string lookupValue)
        {
            var query = "reference.update_lookup";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_id", lookupId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_lookup_value", lookupValue, DbType.String, ParameterDirection.Input);
            await _dataService.ExecuteQuery(query, parameters);
        }

        public async Task UpdateSupplierProductCapabilities(string supplierProductCapabilitiesMatrix)
        {
            var supplierProductCapabilities = JsonConvert.DeserializeObject<DTO.Request.Reference.SupplierProductCapabilityMatrix>(supplierProductCapabilitiesMatrix);
            var query = "reference.update_supplier_product_capabilities";
            var parameters = new DynamicParameters();

            foreach (var supplierProductCapability in supplierProductCapabilities.SupplierProductCapabilityDetailsModel)
            {
                parameters.Add("_supplier_product_capability_id", supplierProductCapability.SupplierProductCapabilityId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("_supplier_id", supplierProductCapability.SupplierId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("_supplier_product_id", supplierProductCapability.SupplierProductId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("_product_capability_id", supplierProductCapability.ProductCapabilityId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("_assurance_date", supplierProductCapability.AssuranceDate, DbType.DateTime2, ParameterDirection.Input);
                parameters.Add("_awaiting_assurance", supplierProductCapability.AwaitingAssurance, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("_provider_assured", supplierProductCapability.ProviderAssured, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("_consumer_assured", supplierProductCapability.ConsumerAssured, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("_capability_version", supplierProductCapability.CapabilityVersion, DbType.String, ParameterDirection.Input);
                await _dataService.ExecuteQuery<SupplierProductCapability>(query, parameters);
            }
        }

        public async Task<Lookup> GetLookupById(int lookupId)
        {
            var query = "reference.get_lookup_by_id";
            var parameters = new DynamicParameters();
            parameters.Add("_lookup_id", lookupId, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<Lookup>(query, parameters);
            return result;
        }

        public async Task<LookupType> GetLookupType(int lookupTypeId)
        {
            if (lookupTypeId > 0)
            {
                var query = "reference.get_lookup_type";
                var parameters = new DynamicParameters();
                parameters.Add("_lookup_type_id", lookupTypeId, DbType.Int16, ParameterDirection.Input);
                var result = await _dataService.ExecuteQueryFirstOrDefault<LookupType>(query, parameters);
                return result;
            }
            return null;
        }
    }
}
