using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class QueryService : IQueryService
    {
        private readonly IDataService _dataService;
        private readonly IReportingService _reportingService;

        public QueryService(IDataService dataService, IReportingService reportingService)
        {
            _dataService = dataService;
            _reportingService = reportingService;
        }

        public async Task<SearchResult> GetSites(SearchRequest searchRequest = null)
        {
            var query = QueryBuilder(searchRequest, "SELECT * FROM Sites WHERE 1=1");
            var result = await _dataService.ExecuteSQLQuery<SearchResultEntry>(query);
            var searchResult = new SearchResult() { SearchResults = result };
            return searchResult;
        }

        public async Task<DataTable> GetSitesForExport(SearchRequest searchRequest = null)
        {
            var sites = await GetSites(searchRequest);
            var json = sites.SearchResults.ConvertObjectToJsonData();
            return json.ConvertJsonDataToDataTable();
        }

        private static string QueryBuilder(SearchRequest searchRequest, string baseQuery)
        {
            var query = new StringBuilder(baseQuery);
            if (searchRequest != null)
            {
                if (searchRequest.ProviderOdsCodeAsList?.Count > 0)
                {
                    var providersCodes = string.Join("','", searchRequest.ProviderOdsCodeAsList);
                    query.Append(" AND SiteODS IN (");
                    query.Append($"'{providersCodes}')");
                }

                if (searchRequest.ProviderNameAsList?.Count > 0)
                {
                    query.Append(" AND (");

                    for (var i = 0; i < searchRequest.ProviderNameAsList.Count; i++)
                    {
                        query.Append($"CHARINDEX('{searchRequest.ProviderNameAsList[i].Trim()}', SiteName) > 0 OR ");
                    }
                    query.Remove(query.Length - 4, 4);
                    query.Append(")");
                }

                query.Append(searchRequest.CCGOdsCode != null ? $" AND CCGODS='{searchRequest.CCGOdsCode}'" : string.Empty);
                query.Append(searchRequest.CCGName != null ? $" AND CCGName='{searchRequest.CCGName}'" : string.Empty);
            }
            query.Append(" ORDER BY 2");
            return query.ToString();
        }
    }
}
