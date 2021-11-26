using gpconnect_user_portal.DAL.Interfaces;
using Microsoft.Extensions.Logging;
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

        public FhirRequestExecution(ILogger<FhirRequestExecution> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> ExecuteFhirQuery<T>(Uri requestUri, CancellationToken cancellationToken) where T : class
        {
            var getRequest = new HttpRequestMessage();
            try
            {
                var client = _httpClientFactory.CreateClient("GpConnectClient");

                getRequest.Method = HttpMethod.Get;
                getRequest.RequestUri = requestUri;

                var response = client.Send(getRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var contents = await response.Content.ReadAsStringAsync(cancellationToken);

                var responseContents = JsonConvert.DeserializeObject<T>(contents);
                return responseContents;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"An exception has occurred while attempting to execute a FHIR API query - {getRequest}");
                throw;
            }
        }
    }
}
