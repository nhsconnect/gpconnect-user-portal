using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

using gpconnect_user_portal.DAL;
using gpconnect_user_portal.DAL.Interfaces;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class HttpClientExtensions
    {
        public static void AddHttpClientServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddHttpClient<IFhirRequestExecution, FhirRequestExecution>(options =>
                {
                    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/fhir+json"));
                    options.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                }
            )
            .AddHttpMessageHandler<FhirApiKeyHeaderHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => CreateHttpMessageHandler(env));
        }

        private static HttpMessageHandler CreateHttpMessageHandler(IWebHostEnvironment env)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            return httpClientHandler;
        }
    }
}
