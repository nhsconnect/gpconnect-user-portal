using System.Net;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class SiteService : ISiteService
{
    private readonly ILogger<SiteService> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerSettings _options;

    public SiteService(ILogger<SiteService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }

    public async Task<SearchResult> SearchSitesAsync(string query, SearchMode mode, int startingIndex, int numberResults)
    {
        try
        {
            var result = default(SearchResult);

            var url = QueryHelpers.AddQueryString("transparency-site", mode.GetQueryStringParameter(), query);

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                {
                    _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
                    throw x.Exception;
                }
                result = JsonConvert.DeserializeObject<SearchResult>(x.Result, _options);
            });

            return result;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "An exception has occurred while attempting to execute an API query - {mode}:{query}", mode, query);
            throw;
        }
    }

    public async Task<SearchResultEntry> SearchSiteAsync(string id)
    {
        try
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };
            var result = default(SearchResultEntry);

            var url = $"transparency-site/{id}";
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);
            
            if (response.StatusCode == HttpStatusCode.NotFound) 
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync(cancellationTokenSource.Token).ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                {
                    _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
                    throw x.Exception;
                }
                result = JsonConvert.DeserializeObject<SearchResultEntry>(x.Result, _options);
            });

            return result;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, $"An exception has occurred while attempting to execute an API query - {id}");
            throw;
        }
    }
}
