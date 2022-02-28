using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class QueryService : IQueryService
    {
        private readonly IDataService _dataService;

        public QueryService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<DTO.Response.Application.Search.SearchResult> GetSites(SearchRequest searchRequest = null)
        {
            var query = "application.find_sites";
            var parameters = new DynamicParameters();
            parameters.Add("_site_name_attribute_name", SearchConstants.SiteNameAttributeName, DbType.String, ParameterDirection.Input);
            parameters.Add("_ccg_ods_code_attribute_name", SearchConstants.CCGOdsCodeAttributeName, DbType.String, ParameterDirection.Input);
            parameters.Add("_ccg_name_attribute_name", SearchConstants.CCGNameAttributeName, DbType.String, ParameterDirection.Input);
            parameters.Add("_html_query_filter_interaction", SearchConstants.HtmlQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_structured_query_filter_interaction", SearchConstants.StructuredQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_appointment_query_filter_interaction", SearchConstants.AppointmentQueryFilterInteraction, DbType.String, ParameterDirection.Input);
            parameters.Add("_send_document_query_filter_interaction", SearchConstants.SendDocumentQueryFilterInteraction, DbType.String, ParameterDirection.Input);            

            if (searchRequest != null)
            {
                if (searchRequest.FilterBy != null)
                {
                    parameters.Add("_filter_by", int.Parse(searchRequest.FilterBy), DbType.Int16, ParameterDirection.Input);
                }
                else
                {
                    parameters.Add("_filter_by", 0, DbType.Int16, ParameterDirection.Input);
                }

                if (searchRequest.SiteOdsCode != null)
                {
                    parameters.Add("_site_ods_code", searchRequest.SiteOdsCode.SearchAndReplace(new Dictionary<string, string> { { ",", "|" }, { " ", "|" } }).Replace("||", "|"), DbType.String, ParameterDirection.Input);
                }
                else
                {
                    parameters.Add("_site_ods_code", null, DbType.String, ParameterDirection.Input);
                }

                if (searchRequest.SiteName != null)
                {
                    parameters.Add("_site_name", searchRequest.SiteName.SearchAndReplace(new Dictionary<string, string> { { ",", "|" } }).Replace("||", "|"), DbType.String, ParameterDirection.Input);
                }
                else
                {
                    parameters.Add("_site_name", null, DbType.String, ParameterDirection.Input);
                }

                if (searchRequest.CCGOdsCode != null)
                {
                    parameters.Add("_ccg_ods_code", searchRequest.CCGOdsCode, DbType.String, ParameterDirection.Input);
                }
                else
                {
                    parameters.Add("_ccg_ods_code", null, DbType.String, ParameterDirection.Input);
                }

                if (searchRequest.CCGName != null)
                {
                    parameters.Add("_ccg_name", searchRequest.CCGName, DbType.String, ParameterDirection.Input);
                }
                else
                {
                    parameters.Add("_ccg_name", null, DbType.String, ParameterDirection.Input);
                }
            }

            var searchResultEntries = await _dataService.ExecuteQuery<DTO.Response.Application.Search.SearchResultEntry>(query, parameters);
            var searchResult = new DTO.Response.Application.Search.SearchResult()
            {
                SearchResultEntries = searchResultEntries
            };
            return searchResult;
        }
    }
}
