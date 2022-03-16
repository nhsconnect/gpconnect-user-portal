using Dapper;
using gpconnect_user_portal.DAL.Enumerations;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Response.Configuration;
using gpconnect_user_portal.Services.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IDataService _dataService;
        
        public ConfigurationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ReferenceApiQuery> GetReferenceApiQuery(ReferenceApiQueryTypes referenceApiQueryType)
        {
            var query = "configuration.get_reference_api_query";
            var parameters = new DynamicParameters();
            parameters.Add("_reference_api_query_type", referenceApiQueryType.ToString(), DbType.String, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<ReferenceApiQuery>(query, parameters);
            return result;
        }

        public async Task<FhirApiQuery> GetFhirApiQuery(FhirApiQueryTypes fhirApiQueryType)
        {
            var query = "configuration.get_fhir_api_query";
            var parameters = new DynamicParameters();
            parameters.Add("_fhir_api_query_type", fhirApiQueryType.ToString(), DbType.String, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<FhirApiQuery>(query, parameters);
            return result;
        }

        public async Task<Spine> GetSpineConfiguration()
        {
            var query = "configuration.get_spine_configuration";
            var result = await _dataService.ExecuteQueryFirstOrDefault<Spine>(query);
            return result;
        }
    }
}
