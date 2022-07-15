using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.AgreementService;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Core.HttpClientServices;

public class AgreementServiceTests
{
    private static string BASE_URI = "http://not-my-address.com";
    
    private readonly Mock<IOptions<AgreementServiceConfig>> _mockOptions;
    private readonly Mock<HttpMessageHandler> _mockMessageHandler;
    private readonly Mock<IAgreementInformationBuilder> _mockAgreementInformationBuilder;

    private readonly IAgreementService _sut;

    public AgreementServiceTests()
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
        
        _mockOptions = new Mock<IOptions<AgreementServiceConfig>>();
        _mockOptions.SetupGet(o => o.Value).Returns(new AgreementServiceConfig
        {
            BaseUrl = BASE_URI
        });

        _mockAgreementInformationBuilder = new Mock<IAgreementInformationBuilder>();

        _sut = new AgreementService(new HttpClient(_mockMessageHandler.Object), _mockAgreementInformationBuilder.Object, _mockOptions.Object);
    }

    [Fact]
    public async Task SubmitAgreementAsync_CallsAgreementInformationBuilder_WithExpectedParameters()
    {
        var expectedOrganisation = "A20027";
        var expectedInteractions = new List<int>();
        var expectedSupplier = "Supplier Name";
        var expectedUseCase = "UseCase";
        var expectedName = "Name";
        var expectedEmail = "Email";
        var expectedPosition = "Position";

        await _sut.SubmitAgreementAsync(expectedOrganisation, expectedSupplier, expectedInteractions, expectedName, expectedEmail, expectedPosition, expectedUseCase);

        _mockAgreementInformationBuilder.Verify(mib => mib.Build(expectedOrganisation, expectedSupplier, expectedUseCase, expectedInteractions, expectedName, expectedEmail, expectedPosition));
    }

    [Fact]
    public async Task SubmitAgreementAsync_CallsHttpClient_WithExpectedParameters()
    {
        var expectedUri = $"{BASE_URI}/agreement";
        var expectedBody = new AgreementInformation
        {
            Interactions = new GpConnectInteractions
            {
                AccessRecordHTMLEnabled = true
            },
            Organisation = new OrganisationInformation
            {
                OdsCode = "Code"
            },
            UseCase = "UseCase",
            SoftwareSupplierName = "SupplierName",
            Signatory = new SignatoryDetails
            {
                Name = "Name",
                Email = "Email",
                Position = "Position"
            }
        };

        _mockAgreementInformationBuilder.Setup(_maib => _maib.Build(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<List<int>>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).Returns(Task.FromResult(expectedBody));

        await _sut.SubmitAgreementAsync(null, null, null, null, null, null, null);

        _mockMessageHandler
            .Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    CheckRequest(req, expectedUri, expectedBody) // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    private bool CheckRequest(HttpRequestMessage req, string expectedUri, AgreementInformation expectedBody)
    {
        var contentBody = JsonConvert.DeserializeObject<AgreementInformation>(req.Content.ReadAsStringAsync().Result);
        return req.Method == HttpMethod.Post
                    && req.RequestUri.AbsoluteUri.ToString() == expectedUri
                    && req.Content.Headers.ContentType.MediaType == "application/json"
                    && contentBody.Organisation.OdsCode == expectedBody.Organisation.OdsCode
                    && contentBody.Signatory.Name == expectedBody.Signatory.Name
                    && contentBody.Signatory.Email == expectedBody.Signatory.Email
                    && contentBody.Signatory.Position == expectedBody.Signatory.Position
                    && contentBody.UseCase == expectedBody.UseCase
                    && contentBody.SoftwareSupplierName == expectedBody.SoftwareSupplierName;
    }

    [Fact]
    public async Task SubmitAgreementAsync_HttpClientThrows_LogsAndThrows()
    {
        _mockMessageHandler
            .Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception("Boom!!!"));
        
        // _mockLogger.Verify();
        await Assert.ThrowsAsync<Exception>(async () => await _sut.SubmitAgreementAsync(null, null, null, null, null, null, null));
    }

    [Fact]
    public async Task SubmitAgreementAsync_HttpClientGetReturnsNon200Response_LogsAndThrows()
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
        
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _sut.SubmitAgreementAsync(null, null, null, null, null, null, null));
    }

    [Fact]
    public async Task SubmitAgreementAsync_HttpClientGetReturns200Response_DoesNotThrow()
    {
        var exception = await Record.ExceptionAsync(() => _sut.SubmitAgreementAsync(null, null, null, null, null, null, null));
        Assert.Null(exception); 
    }
}
