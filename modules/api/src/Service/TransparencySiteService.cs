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

        public const string HtmlQueryFilterInteraction = "gpc.getcarerecord";
        public const string StructuredQueryFilterInteraction = "structured:fhir:rest:read:metadata-1";
        public const string AppointmentQueryFilterInteraction = "appointments-1";
        public const string SendDocumentQueryFilterInteraction = "documents:fhir:rest:read:metadata-1";        

        private readonly IDataService _dataService;
        private readonly ILogger<TransparencySiteService> _logger;

        public TransparencySiteService(IDataService dataService, ILogger<TransparencySiteService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public Task<List<TransparencySite>> GetMatchingSitesAsync(TransparencySiteRequest request)
        {
            var query = "application.find_sites";

            var parameters = BuildBlankSearchParameters();

            parameters.Add("_site_definition_status_min", SiteStatus.LIVE, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_site_definition_status_max", SiteStatus.LIVE, DbType.Int16, ParameterDirection.Input);

            if (!string.IsNullOrWhiteSpace(request.ProviderCode))
            {
                _logger.LogDebug("Preparing Query for ODS based search, {}", request.ProviderCode);
                parameters.Add("_site_ods_code", request.ProviderCode, DbType.String, ParameterDirection.Input);
                parameters.Add("_site_name", null, DbType.String, ParameterDirection.Input);
            }
            else
            {
                _logger.LogDebug("Preparing Query for name based search, {}", request.ProviderName);
                parameters.Add("_site_name", request.ProviderName, DbType.String, ParameterDirection.Input);
                parameters.Add("_site_ods_code", null, DbType.String, ParameterDirection.Input);
            }

            return _dataService.ExecuteQuery<TransparencySite>(query, parameters);        
        }

        private DynamicParameters BuildBlankSearchParameters()
        {
            var parameters = new DynamicParameters();
            
            // Add blank filters as we are not interested in filtering
            parameters.Add("_filter_by", 0, DbType.Int16, ParameterDirection.Input);
            parameters.Add("_html_query_filter_interaction", HtmlQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_structured_query_filter_interaction", StructuredQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_appointment_query_filter_interaction", AppointmentQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_send_document_query_filter_interaction", SendDocumentQueryFilterInteraction, DbType.String, ParameterDirection.Input);            

            // Blank off the ccg fields as not searching by this in MVP
            parameters.Add("_ccg_name", null, DbType.String, ParameterDirection.Input);
            parameters.Add("_ccg_ods_code", null, DbType.String, ParameterDirection.Input);

            return parameters;
        }
    }
}