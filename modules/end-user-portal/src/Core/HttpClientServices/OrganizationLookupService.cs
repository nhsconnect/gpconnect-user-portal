using System.Net;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class OrganizationLookupService: IOrganizationLookupService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerSettings _options;

    public OrganizationLookupService(HttpClient client, IOptions<OrganizationLookupServiceConfig> options)
    {
        _client = client;
        _client.BaseAddress = new UriBuilder(options.Value.BaseUrl).Uri;
        _options = new JsonSerializerSettings 
        {
            NullValueHandling = NullValueHandling.Ignore 
        };
        
    }

    public async Task<Organization> GetOrganizationAsync(string odsCode)
    {
        var response = await _client.GetAsync($"/STU3/Organization/{odsCode}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
 
        var body = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<Organization>(body, _options);
    }

    public class OrganizationLookupServiceConfig
    {
        public string BaseUrl { get; set; } = "";
    }
}