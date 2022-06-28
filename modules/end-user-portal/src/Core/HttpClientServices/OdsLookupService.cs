using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class OrganizationLookupService: IOrganizationLookupService
{
    private readonly HttpClient _client;

    public OrganizationLookupService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Organization> GetOrganizationAsync(string odsCode)
    {
        var response = await _client.GetAsync($"/STU3/Organization/{odsCode}");
        return null;
    }
}