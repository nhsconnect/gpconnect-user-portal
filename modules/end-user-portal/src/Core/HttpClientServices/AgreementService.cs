using System.Net.Http.Headers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class AgreementService: IAgreementService 
{
    private readonly HttpClient _client;
    private readonly IAgreementInformationBuilder _builder;

    public AgreementService(HttpClient client, IAgreementInformationBuilder builder, IOptions<AgreementServiceConfig> options)
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
       
        var content = new StringContent
        (
            JsonConvert.SerializeObject(
                _builder.Build(
                    organisation,
                    supplier,
                    useCase,
                    interactions,
                    signatoryName,
                    signatoryEmail,
                    signatoryPosition
                )
            )
        );

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var response = await _client.PostAsync("/agreement", content);

        response.EnsureSuccessStatusCode();
    }

    public class AgreementServiceConfig 
    {
        public string BaseUrl { get; set; } = "";
    }
}