using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class SiteService : ISiteService
{
    private static ILogger<SiteService> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerSettings _options;

    public SiteService(ILogger<SiteService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }

    public async Task<List<SearchResultEntry>> SearchSitesAsync(SearchRequest searchRequest)
    {
        try
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };
            var result = default(List<SearchResultEntry>);

            var queryDictionary = new Dictionary<string, string>();

            switch (searchRequest.Mode)
            {
                case Helpers.Enumerations.SearchModeEnums.Name:
                    queryDictionary.Add("provider_name", searchRequest.Query);
                    break;
                case Helpers.Enumerations.SearchModeEnums.Code:
                    queryDictionary.Add("provider_code", searchRequest.Query);
                    break;
            }

            var url = UriExtensions.AddQueryParamsToObject("transparency-site", queryDictionary, _options);

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync(cancellationTokenSource.Token).ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                {
                    _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
                    throw x.Exception;
                }
                result = JsonConvert.DeserializeObject<List<SearchResultEntry>>(x.Result, _options);
            });

            return result;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, $"An exception has occurred while attempting to execute an API query - {searchRequest}");
            throw;
        }
    }
}
