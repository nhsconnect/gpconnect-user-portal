using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public class AgreementService: IAgreementService 
{
    private readonly HttpClient _client;
    private readonly IAgreementInformationBuilder _builder;

    public AgreementService(HttpClient client, IAgreementInformationBuilder builder, IOptions<SiteService.SiteServiceConfig> options)
    {
        _client = client;
        _builder = builder;
        _client.BaseAddress = new UriBuilder(options.Value.BaseUrl).Uri;
    }

    public async Task SubmitAgreementAsync(
            OrganisationResult organisation, 
            SoftwareSupplierResult supplier,
            List<GpConnectInteractionForSupplier> interactions,
            string signatoryName, 
            string signatoryEmail, 
            string signatoryPosition, 
            string useCase)
    {
        var response = await _client.PostAsJsonAsync(
            "/agreement",
            _builder.Build(
                organisation,
                supplier,
                useCase,
                interactions,
                signatoryName,
                signatoryEmail,
                signatoryPosition
            )
        );

        response.EnsureSuccessStatusCode();
    }

    public class AgreementServiceConfig 
    {
        public string BaseUrl { get; set; } = "";
    }
}