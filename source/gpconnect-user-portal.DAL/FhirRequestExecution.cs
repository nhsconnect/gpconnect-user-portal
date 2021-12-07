using gpconnect_user_portal.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace gpconnect_user_portal.DAL
{
    public class FhirRequestExecution : IFhirRequestExecution
    {
        private static ILogger<FhirRequestExecution> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<DTO.Response.Configuration.Reference> _configuration;

        public FhirRequestExecution(IOptionsMonitor<DTO.Response.Configuration.Reference> configuration, ILogger<FhirRequestExecution> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<T> ExecuteFhirQuery<T>(string query, CancellationToken cancellationToken) where T : class
        {
            var getRequest = new HttpRequestMessage();
            try
            {
                var client = _httpClientFactory.CreateClient("GpConnectClient");

                getRequest.Method = HttpMethod.Get;
                getRequest.RequestUri = AddHostName(query);

                var response = client.Send(getRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync(cancellationToken);
                    var responseContents = JsonConvert.DeserializeObject<T>(contents);
                    return responseContents;
                }
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"An exception has occurred while attempting to execute a FHIR API query - {getRequest}");
                throw;
            }
        }

        private Uri AddHostName(string query)
        {
            var uriBuilder = new UriBuilder()
            {
                Host = _configuration.CurrentValue.HostName,
                Path = query
            };
            return uriBuilder.Uri;
        }
    }
}
