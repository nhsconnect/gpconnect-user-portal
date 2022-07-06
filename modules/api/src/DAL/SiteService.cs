using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Models;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;

public class SiteService: ISiteService
{
    private readonly IDataService _dataService;
    

    public SiteService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task CreateSiteAttributesAsync(Guid uniqueId, string site_attribute_data)
    {
        var siteAttributeParams = new DynamicParameters();
        siteAttributeParams.Add("_site_unique_identifier", uniqueId, DbType.Guid);
        siteAttributeParams.Add("_site_attributes", site_attribute_data, DbType.String);

        await _dataService.ExecuteQuery("application.add_site_attributes", siteAttributeParams);
    }

    public async Task<SiteDefinition> CreateSiteDefinitionAsync(string odsCode) 
    {   
        var siteDefinitionParams = new DynamicParameters();

        siteDefinitionParams.Add("_site_unique_identifier", Guid.NewGuid(), DbType.Guid);
        siteDefinitionParams.Add("_site_ods_code", odsCode, DbType.String);
        siteDefinitionParams.Add("_site_party_key", null, DbType.String);
        siteDefinitionParams.Add("_site_asid", null, DbType.String);
        siteDefinitionParams.Add("_site_definition_status", 2, DbType.Int16);
        siteDefinitionParams.Add("_site_interactions", "", DbType.String);
        siteDefinitionParams.Add("_master_site_unique_identifier", null, DbType.Guid);

        var siteDefinitions = await _dataService.ExecuteQuery<SiteDefinition>("application.add_site_definition", siteDefinitionParams);

        return siteDefinitions[0];
    }
}
