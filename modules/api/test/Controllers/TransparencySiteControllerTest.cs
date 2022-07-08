using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class TransparencySiteControllerTest
{
    private readonly TransparencySiteController _sut;
    private readonly Mock<ITransparencySiteRequestValidator> _mockValidator;
    private readonly Mock<ITransparencySiteService> _mockService;
    private readonly Mock<ILogger<TransparencySiteController>> _mockLogger;

    // setup
    public TransparencySiteControllerTest()
    {
        _mockValidator = new Mock<ITransparencySiteRequestValidator>();
        _mockService = new Mock<ITransparencySiteService>();
        _mockLogger = new Mock<ILogger<TransparencySiteController>>();

        _sut = new TransparencySiteController(_mockValidator.Object, _mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullValidator_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransparencySiteController(default(ITransparencySiteRequestValidator), _mockService.Object, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransparencySiteController(_mockValidator.Object, default(ITransparencySiteService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransparencySiteController(_mockValidator.Object, _mockService.Object, default(ILogger<TransparencySiteController>)));
    }

    [Fact]
    public async Task Get_WithInvalidProviderCodeSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = default(string?), ProviderName = "Test1", StartPosition = 1, Count = 10 };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithInvalidProviderNameSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = "Test1", ProviderName = default(string?), StartPosition = 1, Count = 10 };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithInvalidStartPositionSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = "Test1", ProviderName = "Test1", StartPosition = default(int?), Count = 10 };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithInvalidNegativeStartPositionSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = "Test1", ProviderName = "Test1", StartPosition = int.MinValue, Count = 10 };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithInvalidCountSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = "Test1", ProviderName = "Test1", StartPosition = 1, Count = default(int?) };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithInvalidNegativeCountSearchParameters_ReturnsBadRequest()
    {
        var query = new TransparencySiteRequest { ProviderCode = "Test1", ProviderName = "Test1", StartPosition = 1, Count = int.MinValue };
        var result = await _sut.Get(query);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithValidSearchParameters_ReturnsOk()
    {
        _mockValidator.Setup(v => v.IsValidRequest(It.IsAny<TransparencySiteRequest>())).Returns(true);
        var query = new TransparencySiteRequest { ProviderCode = "test", StartPosition = 1, Count = 10 };
        var result = await _sut.Get(query);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_WithNullSingleParameter_DoesNotReturnAnException()
    {
        Assert.IsNotType<ArgumentNullException>(async () => await _sut.Get(default(TransparencySiteRequest)));
    }

    [Fact]
    public async Task Get_WithInvalidGuid_ReturnsBadRequest()
    {
        var id = "test1";
        var result = await _sut.GetTransparencySite(id);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithEmptyGuid_ReturnsBadRequest()
    {
        var result = await _sut.GetTransparencySite(default(Guid).ToString());
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WithNull_ReturnsBadRequest()
    {
        var result = await _sut.GetTransparencySite(null);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_CallsValidator_WithExpectedParameters()
    {
        var expected = new TransparencySiteRequest();

        await _sut.Get(expected);

        _mockValidator.Verify(v => v.IsValidRequest(expected), Times.Once);
    }

    [Fact]
    public async Task Get_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidRequest(It.IsAny<TransparencySiteRequest>())).Returns(false);

        var response = await _sut.Get(new TransparencySiteRequest());

        Assert.IsType<BadRequestResult>(response);
    }

    [Fact]
    public async Task Get_WhenValidatorReturnsTrue_CallsService_WithExpectedParameters()
    {
        var expected = new TransparencySiteRequest();

        _mockValidator.Setup(v => v.IsValidRequest(It.IsAny<TransparencySiteRequest>())).Returns(true);

        var response = await _sut.Get(expected);

       _mockService.Verify(s => s.GetMatchingSitesAsync(expected), Times.Once);
    }

    [Fact]
    public void Get_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidRequest(It.IsAny<TransparencySiteRequest>())).Returns(true);
        _mockService.Setup(s => s.GetMatchingSitesAsync(It.IsAny<TransparencySiteRequest>())).ThrowsAsync(new System.Exception("Boom!!!"));

        Assert.ThrowsAsync<Exception>(async () => await _sut.Get(new TransparencySiteRequest()));
    }

    [Fact]
    public void Get_WhenServiceReturns_ReturnsOkResponseWithArray()
    {
        _mockValidator.Setup(v => v.IsValidRequest(It.IsAny<TransparencySiteRequest>())).Returns(true);
        _mockService.Setup(s => s.GetMatchingSitesAsync(It.IsAny<TransparencySiteRequest>())).ReturnsAsync(
            new TransparencySites
            {
                TotalResults = 2,
                Results = new List<TransparencySite>()
                {
                    new TransparencySite() { Name = "Test 1" },
                    new TransparencySite() { Name = "Test 2" }
                }
            }
        );

        var response = _sut.Get(new TransparencySiteRequest());

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as TransparencySites;

        Assert.StrictEqual(2, value?.TotalResults);
        Assert.StrictEqual(2, value?.Results.Count);
        Assert.Equal("Test 1", value?.Results[0].Name);
    }

    [Fact]
    public async Task GetTransparencySite_CallsValidator_WithExpectedParameters()
    {
        var expected = "12341234-1234-1234-1234-123412341234";

        await _sut.GetTransparencySite(expected);

        _mockValidator.Verify(v => v.IsValidId(expected), Times.Once);
    }

    [Fact]
    public async Task GetTransparencySite_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidId(It.IsAny<string>())).Returns(false);

        var response = await _sut.GetTransparencySite("test");

        Assert.IsType<BadRequestResult>(response);
    }    

    [Fact]
    public async Task GetTransparencySite_WhenValidatorReturnsTrue_CallsService_WithExpectedParameters()
    {
        var expected = "12341234-1234-1234-1234-123412341234";

        _mockValidator.Setup(v => v.IsValidId(It.IsAny<string>())).Returns(true);

        var response = await _sut.GetTransparencySite(expected);

       _mockService.Verify(s => s.GetSiteAsync(It.Is<Guid>(g => g.ToString() == expected)), Times.Once);
    }

    [Fact]
    public void GetTransparencySite_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidId(It.IsAny<string>())).Returns(true);
        _mockService.Setup(s => s.GetSiteAsync(It.IsAny<Guid>())).ThrowsAsync(new System.Exception("Boom!!!"));

        Assert.ThrowsAsync<Exception>(async () => await _sut.GetTransparencySite("12341234-1234-1234-1234-123412341234"));
    }

    [Fact]
    public void GetTransparencySite_WhenServiceReturns_ReturnsOkResponseWithObject()
    {
        _mockValidator.Setup(v => v.IsValidId(It.IsAny<string>())).Returns(true);
        _mockService.Setup(s => s.GetSiteAsync(It.IsAny<Guid>())).ReturnsAsync(
            new TransparencySite { Name = "Test"}
        );

        var response = _sut.GetTransparencySite("12341234-1234-1234-1234-123412341234");

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as TransparencySite;

        Assert.Equal("Test", value?.Name);
    }
}
