using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler;

public static class HttpClientExtensions
{
  public static void AddHttpClientServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    services.AddHttpClient<IRequestService, RequestService>(options =>
    {
      options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/fhir+json"));
      options.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
      options.BaseAddress = ValidBaseAddress(configuration["API_BASEADDRESS"]);
    }).AddPolicyHandler(GetRetryPolicy())
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .ConfigurePrimaryHttpMessageHandler(() => CreateHttpMessageHandler(env));
  }

  private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
  {
    return HttpPolicyExtensions
        .HandleTransientHttpError()
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
    if (Uri.IsWellFormedUriString(baseAddress, UriKind.Absolute))
    {
      return new Uri(baseAddress);
    }
    throw new ArgumentException("API Base Address is not well formed", baseAddress);
  }
}
