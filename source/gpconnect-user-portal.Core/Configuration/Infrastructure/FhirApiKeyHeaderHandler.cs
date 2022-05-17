using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

// Testing https://stackoverflow.com/a/65579261
namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public class FhirApiKeyHeaderHandler: DelegatingHandler
    {
        private const string FhirApiKeyHeaderName = "apikey";
        private readonly IOptions<object> _config;

        public FhirApiKeyHeaderHandler(IOptions<object> config)
        {
            _config = config;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(FhirApiKeyHeaderName)) 
            {
                request.Headers.Add(FhirApiKeyHeaderName, _config.Value.ToString());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}