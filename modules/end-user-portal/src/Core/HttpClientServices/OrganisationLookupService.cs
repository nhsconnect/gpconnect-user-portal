using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class OrganisationLookupService : IOrganisationLookupService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerSettings _options;

    public OrganisationLookupService(HttpClient client, IOptions<OrganisationLookupServiceConfig> options)
    {
        _client = client;
        _client.BaseAddress = new UriBuilder(options.Value.BaseUrl).Uri;
        _options = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
    }

    public async Task<OrganisationResult> GetOrganisationAsync(string odsCode)
    {
        var response = await _client.GetAsync($"/STU3/Organization/{odsCode}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<OrganisationResult>(body, _options);
    }

    public class OrganisationLookupServiceConfig
    {
        public string BaseUrl { get; set; } = "";
    }
}
