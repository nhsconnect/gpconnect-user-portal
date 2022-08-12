using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            string organisationOdsCode, 
            string supplierId,
            List<Helpers.Constants.GpConnectInteractions> interactions,
            string signatoryName, 
            string signatoryEmail, 
            string signatoryPosition, 
            string useCase)
    {

        var agreementInformation = await _builder.Build(organisationOdsCode, supplierId, useCase, interactions, signatoryName, signatoryEmail, signatoryPosition);

        var jsonContent = new StringContent(JsonConvert.SerializeObject(agreementInformation));

        jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var response = await _client.PostAsync("/agreement", jsonContent);

        response.EnsureSuccessStatusCode();
    }

    public class AgreementServiceConfig 
    {
        public string BaseUrl { get; set; } = "";
    }
}
