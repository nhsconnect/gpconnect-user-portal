using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public static class HttpClientExtensions
{
  public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    Action<HttpClient> httpClientConfig = options =>
    {
      options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/fhir+json"));
      options.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
      options.BaseAddress = ValidBaseAddress(configuration["API_BASEADDRESS"]);
    };

    services.AddHttpClient<ISiteService, SiteService>(httpClientConfig).AugmentHttpClientBuilder(env);
  }

  private static IHttpClientBuilder AugmentHttpClientBuilder(this IHttpClientBuilder httpClientBuilder, IWebHostEnvironment env)
  {
    return httpClientBuilder.AddPolicyHandler(GetRetryPolicy())
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .ConfigurePrimaryHttpMessageHandler(() => CreateHttpMessageHandler(env));
  }

  private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
  {
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
  }

  private static HttpMessageHandler CreateHttpMessageHandler(IWebHostEnvironment env)
  {
    var httpClientHandler = new HttpClientHandler();
    httpClientHandler.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
    return httpClientHandler;
  }

  private static Uri ValidBaseAddress(string baseAddress)
  {
    try
    {
      var uriBuilder = new UriBuilder(baseAddress);
      return uriBuilder.Uri;
    }
    catch(ArgumentException ex)
    {
      throw ex;
    }    
  }
}