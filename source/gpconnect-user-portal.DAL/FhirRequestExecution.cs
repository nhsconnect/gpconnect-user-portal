using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.Helpers;
using Microsoft.Extensions.Configuration;
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
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FhirRequestExecution(IConfiguration configuration, ILogger<FhirRequestExecution> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<T> ExecuteFhirQuery<T>(string query, CancellationToken cancellationToken, string hostName = null, string apiKey = null) where T : class
        {
            var getRequest = new HttpRequestMessage 
            {
                Method = HttpMethod.Get,
                RequestUri = AddHostName(hostName, query),
            };
            try
            {
                var response = _httpClient.Send(getRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

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

        private Uri AddHostName(string hostName, string query)
        {
            var baseAddress = StringExtensions.Coalesce(hostName, _configuration.GetSection("Reference:HostName").Value);
            return new Uri($"{AddScheme(baseAddress)}{query}");
        }

        private string AddScheme(string baseAddress)
        {
            return !baseAddress.StartsWith("https://") ? "https://" + baseAddress : baseAddress;
        }
    }
}
