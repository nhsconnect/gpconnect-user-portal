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
            string supplierName,
            List<int> interactions,
            string signatoryName, 
            string signatoryEmail, 
            string signatoryPosition, 
            string useCase)
    {

        var jsonContent = await _builder.Build(organisationOdsCode, supplierName, useCase, interactions, signatoryName, signatoryEmail, signatoryPosition);

        var content = new StringContent
        (
            JsonConvert.SerializeObject(
                jsonContent
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
