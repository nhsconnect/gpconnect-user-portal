using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public class HttpClientExtensions
    {
        public HttpClientExtensions()
        {            
        }        

        public void AddHttpClientServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddHttpClient("GpConnectClient", options =>
            {
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/fhir+json"));
                options.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            }).ConfigurePrimaryHttpMessageHandler(() => CreateHttpMessageHandler(env));
        }

        private HttpMessageHandler CreateHttpMessageHandler(IWebHostEnvironment env)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            return httpClientHandler;
        }
    }
}
