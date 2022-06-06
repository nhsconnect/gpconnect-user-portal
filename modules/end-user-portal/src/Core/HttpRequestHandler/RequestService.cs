using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler;

public class RequestService : IRequestService
{
  private static ILogger<RequestService> _logger;
  private readonly HttpClient _httpClient;
  private readonly JsonSerializerSettings _options;

  public RequestService(ILogger<RequestService> logger, HttpClient httpClient)
  {
    _logger = logger;
    _httpClient = httpClient;
    _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
  }

  public async Task<TResult> ExecuteApiGetAsync<TResult>(string url) where TResult : class
  {
    var cancellationTokenSource = new CancellationTokenSource();
    var request = new HttpRequestMessage()
    {
      Method = HttpMethod.Get
    };
    var result = default(TResult);

    try
    {
      var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);
      response.EnsureSuccessStatusCode();

      await response.Content.ReadAsStringAsync(cancellationTokenSource.Token).ContinueWith((Task<string> x) =>
      {
        if (x.IsFaulted)
          throw x.Exception;

        result = JsonConvert.DeserializeObject<TResult>(x.Result, _options);
      });

      return result;
    }
    catch (Exception exc)
    {
      _logger.LogError(exc, $"An exception has occurred while attempting to execute an API query - {request}");
      throw;
    }
  }

  public async Task<TResult> ExecuteApiGetAsync<TRequest, TResult>(TRequest t, string url) where TRequest : class where TResult : class
  {
    var cancellationTokenSource = new CancellationTokenSource();
    var request = new HttpRequestMessage()
    {
      Method = HttpMethod.Get
    };
    var result = default(TResult);

    var jsonRequest = JsonConvert.SerializeObject(t, _options);
    var queryParams = JsonConvert.DeserializeObject<Dictionary<string, string?>>(jsonRequest);
    if (queryParams != null)
    {
      url = QueryHelpers.AddQueryString(url, queryParams);
    }

    try
    {
      var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationTokenSource.Token);
      response.EnsureSuccessStatusCode();

      await response.Content.ReadAsStringAsync(cancellationTokenSource.Token).ContinueWith((Task<string> x) =>
      {
        if (x.IsFaulted)
        {
          _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
          throw x.Exception;
        }
        result = JsonConvert.DeserializeObject<TResult>(x.Result, _options);
      });

      return result;
    }
    catch (Exception exc)
    {
      _logger.LogError(exc, $"An exception has occurred while attempting to execute an API query - {request}");
      throw;
    }
  }

  public Task<TResult> ExecuteApiPostAsync<TRequest, TResult>(TRequest t, string url) where TRequest : class where TResult : class
  {
    throw new NotImplementedException();
  }
}
