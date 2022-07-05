using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.OrganizationLookupService;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class OrganizationLookupServiceTests
{
    private static string BASE_URI = "http://not-my-address.com";
    
    private static string EXAMPLE_RESPONSE = @"{
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
    
    private readonly Mock<IOptions<OrganizationLookupServiceConfig>> _mockOptions;
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
        
        _mockOptions = new Mock<IOptions<OrganizationLookupServiceConfig>>();
        _mockOptions.SetupGet(o => o.Value).Returns(new OrganizationLookupServiceConfig
        {
            BaseUrl = BASE_URI
        });

        _sut = new OrganizationLookupService(new HttpClient(_mockMessageHandler.Object), _mockOptions.Object);
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

    [Fact]
    public async Task GetOrganizationDetailsAsync_HttpClientThrows_LogsAndThrows()
    {
        _mockMessageHandler
            .Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception("Boom!!!"));
        
        // _mockLogger.Verify();
        await Assert.ThrowsAsync<Exception>(async () => await _sut.GetOrganizationAsync("test"));
    }

    [Fact]
    public async Task GetOrganizationDetailsAsync_HttpClientGetReturnsNotFound_LogsAndReturnsNull()
    {
        _mockMessageHandler
            .Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(string.Empty),
                    StatusCode = HttpStatusCode.NotFound
                };

                return Task.FromResult(response);
            });
        
        var result = await _sut.GetOrganizationAsync("test");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetOrganizationDetailsAsync_HttpClientGetReturnsNon200Response_LogsAndThrows()
    {
        _mockMessageHandler
            .Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(string.Empty),
                    StatusCode = HttpStatusCode.Forbidden
                };

                return Task.FromResult(response);
            });
        
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _sut.GetOrganizationAsync("test"));
    }

    [Fact]
    public async Task GetOrganizationDetailsAsync_DeserialiserCannotParseResponse_Throws()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent("{"),
                    StatusCode = HttpStatusCode.OK
                };

                return Task.FromResult(response);
            });

        await Assert.ThrowsAsync<JsonSerializationException>(async () => await _sut.GetOrganizationAsync("test"));
    }

    [Fact]
    public async Task GetOrganizationDetailsAsync_HttpClientGetReturns200Response_ReturnsDeserializedResponse()
    {
        var result = await _sut.GetOrganizationAsync("test");

        Assert.NotNull(result);
        Assert.Equal("FKG31", result.Id);
        Assert.Equal("QUEENS PHARMACY", result.Name);
        Assert.Equal(2, result.Address.Lines.Count);
        Assert.Equal("12 QUEENSTOWN ROAD", result.Address.Lines[0]);
        Assert.Equal("BATTERSEA", result.Address.Lines[1]);
        Assert.Equal("LONDON", result.Address.City);
        Assert.Equal("GREATER LONDON", result.Address.County);
        Assert.Equal("SW8 3RX", result.Address.Postcode);
        Assert.Equal("ENGLAND", result.Address.Country);
    }
}
