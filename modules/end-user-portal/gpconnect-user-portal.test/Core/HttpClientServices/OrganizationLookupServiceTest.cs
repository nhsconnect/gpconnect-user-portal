using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Moq;
using Moq.Protected;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class OrganizationLookupServiceTests
{
    private static string BASE_URI = "http://not-my-address.com";
    private static string EXAMPLE_RESPONSE = @"
        ""id"": ""FKG31"",
        ""name"": ""QUEENS PHARMACY"",
        ""address"": {
            ""line"": [
                ""12 QUEENSTOWN ROAD"",
                ""BATTERSEA""
            ],
            ""city"": ""LONDON"",
            ""district"": ""GREATER LONDON"",
            ""postalCode"": ""SW8 3RX"",
            ""country"": ""ENGLAND""
        }
    }";

    private readonly Mock<HttpMessageHandler> _mockMessageHandler;
    private readonly IOrganizationLookupService _sut;

    public OrganizationLookupServiceTests()
    {
        _mockMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(EXAMPLE_RESPONSE),
                    StatusCode = HttpStatusCode.OK
                };

                return Task.FromResult(response);
            })
            .Verifiable();
        
        _sut = new OrganizationLookupService(new HttpClient(_mockMessageHandler.Object)
        {
            BaseAddress = new Uri(BASE_URI)
        });
    }

    [Fact]
    public async Task GetOrganizationDetailsAsync_CallsHttpClient_WithExpectedParameters()
    {
        var odsCode = "TEST1";
        var expectedUri = $"{BASE_URI}/STU3/Organization/{odsCode}";

        await _sut.GetOrganizationAsync(odsCode);

        _mockMessageHandler
            .Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri.ToString() == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
