using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class AgreementControllerTest
{
    private readonly AgreementController _sut;
    private readonly Mock<IAgreementService> _mockService;

    public AgreementControllerTest()
    {
        _mockService = new Mock<IAgreementService>(); 

        _sut = new AgreementController(_mockService.Object, (new Mock<ILogger<AgreementController>>()).Object);
    }

    [Fact]
    public async Task CreateAgreementEntity_CallsService_WithExpectedParameters()
    {
        var expected = new AgreementInformation();

        await _sut.CreateAgreementEntity(expected);

        _mockService.Verify(v => v.CreateAgreementAsync(expected), Times.Once);
    }

    [Fact]
    public void Get_WhenServiceThrows_ThrowsException()
    {
        _mockService.Setup(s => s.CreateAgreementAsync(It.IsAny<AgreementInformation>())).ThrowsAsync(new Exception("Boom!!!"));

        Assert.ThrowsAsync<Exception>(async () => await _sut.CreateAgreementEntity(new AgreementInformation()));
    }

    [Fact]
    public void Get_WhenServiceReturns_Returns201CreatedResponse()
    {
        var response = _sut.CreateAgreementEntity(new AgreementInformation());

        Assert.NotNull(response.Result);
        Assert.IsType<StatusCodeResult>(response.Result);

        var result = response.Result as StatusCodeResult;
        Assert.StrictEqual(201, result?.StatusCode);
    }
}
