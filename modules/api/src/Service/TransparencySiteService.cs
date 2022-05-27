using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service
{
    public class TransparencySiteService: ITransparencySiteService
    {
        private enum SiteStatus {
            LIVE = 5,
        }
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public TransparencySiteService(IDataService dataService, ILogger logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public Task<List<TransparencySite>> GetMatchingSitesAsync(TransparencySiteRequest request)
        {
            var query = "application.find_sites";

            var parameters = new DynamicParameters();
            parameters.Add("_site_definition_status_min", SiteStatus.LIVE, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_site_definition_status_max", SiteStatus.LIVE, DbType.Int16, ParameterDirection.Input);

            if (!string.IsNullOrWhiteSpace(request.ProviderCode))
            {
                _logger.LogDebug("Preparing Query for ODS based search, {}", request.ProviderCode);
                parameters.Add("_site_ods_code", request.ProviderCode, DbType.String, ParameterDirection.Input);
            }
            else
            {
                _logger.LogDebug("Preparing Query for name based search, {}", request.ProviderName);
                parameters.Add("_site_name", request.ProviderName, DbType.String, ParameterDirection.Input);
            }
         
            return _dataService.ExecuteQuery<TransparencySite>(query, parameters);        
        }
    }
}