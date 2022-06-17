
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class SiteServiceTests 
{
    private static string BASE_URI = "http://not-my-address.com";

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
                    Content = new StringContent("[]"),
                    StatusCode = HttpStatusCode.OK
                };

                //...decide what to put in the response after looking at the contents of the request

                return Task.FromResult(response);
            })
            .Verifiable();
        
        _sut = new SiteService(Mock.Of<ILogger<SiteService>>(), new HttpClient(_mockMessageHandler.Object)
        {
            BaseAddress = new Uri(BASE_URI)
        });
    }

    [Theory]
    [InlineData(SearchMode.Name, "provider_name")]
    [InlineData(SearchMode.Code, "provider_code")]
    public async Task SearchSitesAsync_CallsHttpClient_WithExpectedParameters(SearchMode mode, string expectedParameterName)
    {
        Uri expectedUri = new Uri($"{BASE_URI}/transparency-site?{expectedParameterName}=Query");

        await _sut.SearchSitesAsync("Query", mode);

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

        Assert.ThrowsAsync<Exception>(async () => await _sut.SearchSitesAsync("Query", SearchMode.Name));
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

        Assert.ThrowsAsync<HttpRequestException>(async () => await _sut.SearchSitesAsync("Query", SearchMode.Name));
    }

    [Fact]
    public async Task SearchSitesAsync_HttpClientReturns200OK_ReturnsJsonReponseAsList()
    {
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => (
                Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent("[{\"id\":\"12341234-1234-1234-1234-123412341234\"}]"),
                    StatusCode = HttpStatusCode.OK
                })
            ));

        var result = await _sut.SearchSitesAsync("Query", SearchMode.Name);

        Assert.StrictEqual(1, result.Count);
        Assert.Equal("12341234-1234-1234-1234-123412341234", result[0].SiteDefinitionId);
    }
}