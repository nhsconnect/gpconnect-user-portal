using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class CareSettingControllerTest
{
    private readonly CareSettingController _sut;
    private readonly Mock<ICareSettingRequestValidator> _mockValidator;
    private readonly Mock<ICareSettingService> _mockService;
    private readonly Mock<ILogger<CareSettingController>> _mockLogger;

    // setup
    public CareSettingControllerTest()
    {
        _mockValidator = new Mock<ICareSettingRequestValidator>();
        _mockService = new Mock<ICareSettingService>();
        _mockLogger = new Mock<ILogger<CareSettingController>>();

         _sut = new CareSettingController(_mockValidator.Object, _mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetList_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetCareSettings()).ReturnsAsync(new List<CareSetting>
            {
                new CareSetting { CareSettingId = 1, CareSettingName = "Care Setting Name 1", CareSettingDescription = "Care Setting Description 1", CareSettingValue = "A&E"},
                new CareSetting { CareSettingId = 2, CareSettingName = "Care Setting Name 2", CareSettingDescription = "Care Setting Description 2", CareSettingValue = "Dental Practice"}
            });

        var response = await _sut.Get();

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<CareSetting>;

        Assert.StrictEqual(2, value?.ToList().Count);
        Assert.Equal("A&E", value?.ToList()[0].CareSettingValue);
    }

    [Fact]
    public async Task GetSingle_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetCareSetting(It.IsAny<int>()))
            .ReturnsAsync(new CareSetting { CareSettingId = 1, CareSettingName = "Care Setting Name 1", CareSettingDescription = "Care Setting Description 1", CareSettingValue = "Accident & Emergency" });

        var response = await _sut.Get(1);

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as CareSetting;

        Assert.Equal("Accident & Emergency", value?.CareSettingValue);
    }

    [Fact]
    public async Task Update_CallsValidator_WithExpectedParameters()
    {
        var expected = new CareSettingUpdateRequest();

        await _sut.Put(It.IsAny<int>(), expected);

        _mockValidator.Verify(v => v.IsValidUpdate(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_CallsValidator_WithExpectedParameters()
    {
        var expected = new CareSettingDisableRequest();

        await _sut.Put(It.IsAny<int>(), expected);

        _mockValidator.Verify(v => v.IsValidDisable(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<CareSettingUpdateRequest>())).Returns(false);
        var response = await _sut.Put(It.IsAny<int>(), new CareSettingUpdateRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<CareSettingDisableRequest>())).Returns(false);
        var response = await _sut.Put(It.IsAny<int>(), new CareSettingDisableRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new CareSettingUpdateRequest();
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<CareSettingUpdateRequest>())).Returns(true);

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.UpdateCareSetting(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new CareSettingDisableRequest();
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<CareSettingDisableRequest>())).Returns(true);

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.DisableCareSetting(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<CareSettingUpdateRequest>())).Returns(true);
        _mockService.Setup(s => s.UpdateCareSetting(It.IsAny<CareSettingUpdateRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new CareSettingUpdateRequest()));
    }

    [Fact]
    public async Task Disable_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<CareSettingDisableRequest>())).Returns(true);
        _mockService.Setup(s => s.DisableCareSetting(It.IsAny<CareSettingDisableRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new CareSettingDisableRequest()));
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<CareSettingAddRequest>())).Returns(false);
        var response = await _sut.Post(new CareSettingAddRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<CareSettingAddRequest>())).Returns(true);
        var response = await _sut.Post(new CareSettingAddRequest());
        Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<CareSettingAddRequest>())).Returns(true);
        _mockService.Setup(s => s.AddCareSetting(It.IsAny<CareSettingAddRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Post(new CareSettingAddRequest()));
    }

}
