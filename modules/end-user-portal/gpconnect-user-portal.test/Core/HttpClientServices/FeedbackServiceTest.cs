using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.FeedbackService;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class FeedbackServiceTests
{
    private static string BASE_URI = "http://not-my-address.com";
    
    private readonly Mock<IOptions<FeedbackServiceConfig>> _mockOptions;
    private readonly Mock<HttpMessageHandler> _mockMessageHandler;
    private readonly IFeedbackService _sut;

    public FeedbackServiceTests()
    {
        _mockMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(""),
                    StatusCode = HttpStatusCode.OK
                };

                return Task.FromResult(response);
            })
            .Verifiable();
        
        _mockOptions = new Mock<IOptions<FeedbackServiceConfig>>();
        _mockOptions.SetupGet(o => o.Value).Returns(new FeedbackServiceConfig
        {
            BaseUrl = BASE_URI
        });

        _sut = new FeedbackService(new HttpClient(_mockMessageHandler.Object), _mockOptions.Object);
    }

    [Fact]
    public async Task SubmitAgreementAsync_CallsHttpClient_WithExpectedParameters()
    {
        var expectedUri = $"{BASE_URI}/feedback";
        var expectedBody = new FeedbackInformation
        {
            OverallRating = "Very satisfied",
            ImproveService = "Excellent"
        };

        await _sut.SubmitFeedbackAsync(expectedBody.OverallRating, expectedBody.ImproveService);

        _mockMessageHandler
            .Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => CheckRequest(req, expectedUri, expectedBody)),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    private bool CheckRequest(HttpRequestMessage req, string expectedUri, FeedbackInformation expectedBody)
    {
        var contentBody = JsonConvert.DeserializeObject<FeedbackInformation>(req.Content.ReadAsStringAsync().Result);
        return req.Method == HttpMethod.Post
                    && req.RequestUri.AbsoluteUri.ToString() == expectedUri
                    && req.Content.Headers.ContentType.MediaType == "application/json"
                    && contentBody.OverallRating == expectedBody.OverallRating
                    && contentBody.ImproveService == expectedBody.ImproveService;
    }

    [Fact]
    public async Task SubmitFeedbackAsync_HttpClientThrows_LogsAndThrows()
    {
        _mockMessageHandler
            .Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception("Boom!!!"));
        
        await Assert.ThrowsAsync<Exception>(async () => await _sut.SubmitFeedbackAsync(null, null));
    }

    [Fact]
    public async Task SubmitFeedbackAsync_HttpClientGetReturnsNon200Response_LogsAndThrows()
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
        
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _sut.SubmitFeedbackAsync(null, null));
    }

    [Fact]
    public async Task SubmitFeedbackAsync_HttpClientGetReturns200Response_DoesNotThrow()
    {
        var exception = await Record.ExceptionAsync(() => _sut.SubmitFeedbackAsync(null, null));
        Assert.Null(exception); 
    }
}
