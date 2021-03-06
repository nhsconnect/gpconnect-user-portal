using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Helpers;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class TransparencySiteService : ITransparencySiteService
{
    private readonly IDataService _dataService;
    private readonly ILogger<TransparencySiteService> _logger;

    public TransparencySiteService(IDataService dataService, ILogger<TransparencySiteService> logger)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TransparencySites> GetMatchingSitesAsync(TransparencySiteRequest request)
    {
        var query = "application.find_sites";

        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(request.ProviderCode))
        {
            _logger.LogDebug("Preparing Query for ODS based search, {}", request.ProviderCode);
            parameters.Add("_site_ods_code", request.ProviderCode.SearchAndReplace(new Dictionary<string, string> { { ",", "|" }, { " ", "|" }, { "| ", "|" } }).Replace("||", "|"), DbType.String, ParameterDirection.Input);
            parameters.Add("_site_name", null, DbType.String, ParameterDirection.Input);
        }
        else if (!string.IsNullOrWhiteSpace(request.ProviderName))
        {
            _logger.LogDebug("Preparing Query for name based search, {}", request.ProviderName);
            parameters.Add("_site_name", request.ProviderName.SearchAndReplace(new Dictionary<string, string> { { ",", "|" }, { "| ", "|" } }).Replace("||", "|"), DbType.String, ParameterDirection.Input);
            parameters.Add("_site_ods_code", null, DbType.String, ParameterDirection.Input);
        }

        var transparencySiteCount = await GetTransparencySiteCount(parameters);

        //Only add the pagination parameters when we are returning the actual matching sites
        parameters.Add("_start_position", request.StartPosition, DbType.Int32, ParameterDirection.Input);
        parameters.Add("_number_to_return", request.Count, DbType.Int32, ParameterDirection.Input);

        var transparencySite = await _dataService.ExecuteQuery<TransparencySite>(query, parameters);
        var transparencySites = new TransparencySites() { Results = transparencySite, TotalResults = transparencySiteCount };
        return transparencySites;
    }

    private async Task<int> GetTransparencySiteCount(DynamicParameters parameters)
    {
        var query = "application.find_sites_count";
        var transparencySiteCount = await _dataService.ExecuteScalar(query, parameters);
        return transparencySiteCount;
    }

    public Task<TransparencySite> GetSiteAsync(Guid id)
    {
        var query = "application.find_site";

        var parameters = new DynamicParameters();
        parameters.Add("_site_unique_identifier", id, DbType.Guid, ParameterDirection.Input);

        return _dataService.ExecuteQueryFirstOrDefault<TransparencySite>(query, parameters);
    }
}
