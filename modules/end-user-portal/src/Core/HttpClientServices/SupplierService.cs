using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class SupplierService : ISupplierService
{
    private readonly ILogger<SupplierService> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerSettings _options;

    public SupplierService(ILogger<SupplierService> logger, HttpClient httpClient, IOptions<SiteService.SiteServiceConfig> options)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new UriBuilder(options.Value.BaseUrl).Uri;
        _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }

    public async Task<List<SoftwareSupplierResult>> GetSoftwareSuppliersAsync()
    {
        try
        {
            var result = default(List<SoftwareSupplierResult>);
            var url = "supplier";
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                {
                    _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
                    throw x.Exception;
                }
                result = JsonConvert.DeserializeObject<List<SoftwareSupplierResult>>(x.Result, _options);
            });
            return result;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "An exception has occurred while attempting to execute an API query");
            throw;
        }
    }

    public async Task<SoftwareSupplierResult> GetSoftwareSupplierAsync(int supplierId)
    {
        try
        {
            var result = default(SoftwareSupplierResult);
            var url = $"supplier/{supplierId}";

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                {
                    _logger.LogError(x.Exception, $"A serialization error occurred in trying to read the response from an API query");
                    throw x.Exception;
                }
                result = JsonConvert.DeserializeObject<SoftwareSupplierResult>(x.Result, _options);
            });

            return result;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "An exception has occurred while attempting to execute an API query");
            throw;
        }
    }
}
