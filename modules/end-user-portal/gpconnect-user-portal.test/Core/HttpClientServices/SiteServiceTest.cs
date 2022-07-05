using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.SiteService;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class SiteServiceTests 
{
    private static string BASE_URI = "http://not-my-address.com";
    private Mock<IOptions<SiteServiceConfig>> _mockOptions;
    private readonly Mock<HttpMessageHandler> _mockMessageHandler;
    private readonly SiteService _sut;

    public SiteServiceTests()
    {
        _mockMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent("{\"totalResults\": 0, \"results\": []}"),
                    StatusCode = HttpStatusCode.OK
                };

                return Task.FromResult(response);
            })
            .Verifiable();
        
        _mockOptions = new Mock<IOptions<SiteServiceConfig>>();
        _mockOptions.SetupGet(o => o.Value).Returns(new SiteServiceConfig
        {
            BaseUrl = BASE_URI
        });

        _sut = new SiteService(Mock.Of<ILogger<SiteService>>(), new HttpClient(_mockMessageHandler.Object), _mockOptions.Object);
    }

    [Theory]
    [InlineData(SearchMode.Name, "provider_name", 1, 30)]
    [InlineData(SearchMode.Code, "provider_code", 2, 75)]
    public async Task SearchSitesAsync_CallsHttpClient_WithExpectedParameters(SearchMode mode, string expectedParameterName, int expectedStart, int expectedLength)
    {
        Uri expectedUri = new Uri($"{BASE_URI}/transparency-site?{expectedParameterName}=Query&start={expectedStart}&count={expectedLength}");

        await _sut.SearchSitesAsync("Query", mode, expectedStart, expectedLength);

        _mockMessageHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(1), // we expected a single external request
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get  // we expected a GET request
                && req.RequestUri == expectedUri // to this uri
            ),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public void SearchSitesAsync_HttpClientThrows_Throws()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception("Boom!"));

        Assert.ThrowsAsync<Exception>(async () => await _sut.SearchSitesAsync("Query", SearchMode.Name, 0, int.MaxValue));
    }

    [Fact]
    public void SearchSitesAsync_HttpClientReturnsNon200Code_Throws()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent(""),
                    StatusCode = HttpStatusCode.NotFound
                })
            ));

        Assert.ThrowsAsync<HttpRequestException>(async () => await _sut.SearchSitesAsync("Query", SearchMode.Name, 0, int.MaxValue));
    }

    [Fact]
    public async Task SearchSitesAsync_HttpClientReturns200OK_ReturnsJsonReponseAsSearchResult()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent("{\"totalResults\": 1, \"results\": [{\"id\":\"12341234-1234-1234-1234-123412341234\"}]}"),
                    StatusCode = HttpStatusCode.OK
                })
            ));

        var result = await _sut.SearchSitesAsync("Query", SearchMode.Name, 0, int.MaxValue);

        Assert.StrictEqual(1, result.SearchResults.Count);
        Assert.Equal("12341234-1234-1234-1234-123412341234", result.SearchResults[0].SiteDefinitionId);
    }

    [Fact]
    public async Task SearchSiteAsync_CallsHttpClient_WithExpectedParameters()
    {
        var expectedId = "id";

        Uri expectedUri = new Uri($"{BASE_URI}/transparency-site/{expectedId}");

        await _sut.SearchSiteAsync(expectedId);

        _mockMessageHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(1), // we expected a single external request
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get  // we expected a GET request
                && req.RequestUri == expectedUri // to this uri
            ),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public void SearchSiteAsync_HttpClientThrows_Throws()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception("Boom!"));

        Assert.ThrowsAsync<Exception>(async () => await _sut.SearchSiteAsync("Id"));
    }

    [Fact]
    public async Task SearchSiteAsync_HttpClientReturns_NotFound_ReturnsNull()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent(""),
                    StatusCode = HttpStatusCode.NotFound
                })
            ));

        var result = await _sut.SearchSiteAsync("Id");

        Assert.Null(result);
    }

    [Fact]
    public void SearchSiteAsync_HttpClientReturnsUnexpectedErrorCode_Throws()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent(""),
                    StatusCode = HttpStatusCode.Forbidden
                })
            ));

        Assert.ThrowsAsync<Exception>(async () => await _sut.SearchSiteAsync("Id"));
    }

    [Fact]
    public async Task SearchSiteAsync_HttpClientReturns200OK_ReturnsJsonReponseAsSearchResult()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent("{\"id\":\"12341234-1234-1234-1234-123412341234\"}"),
                    StatusCode = HttpStatusCode.OK
                })
            ));

        var result = await _sut.SearchSiteAsync("Id");

        Assert.Equal("12341234-1234-1234-1234-123412341234", result.SiteDefinitionId);
    }
}
