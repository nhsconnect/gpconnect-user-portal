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

public class SupplierControllerTest
{
    private readonly SupplierController _sut;
    private readonly Mock<ISupplierRequestValidator> _mockValidator;
    private readonly Mock<ISupplierService> _mockService;
    private readonly Mock<ILogger<SupplierController>> _mockLogger;

    // setup
    public SupplierControllerTest()
    {
        _mockValidator = new Mock<ISupplierRequestValidator>();
        _mockService = new Mock<ISupplierService>();
        _mockLogger = new Mock<ILogger<SupplierController>>();

         _sut = new SupplierController(_mockValidator.Object, _mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetList_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetSuppliers()).ReturnsAsync(new List<Supplier>
            {
                new Supplier { SupplierId = 1, SupplierName = "Supplier Name 1", SupplierDescription = "Supplier Description 1", SupplierValue = "EMIS"},
                new Supplier { SupplierId = 2, SupplierName = "Supplier Name 2", SupplierDescription = "Supplier Description 2", SupplierValue = "TPP"}
            });

        var response = await _sut.Get();

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<Supplier>;

        Assert.StrictEqual(2, value?.ToList().Count);
        Assert.Equal("EMIS", value?.ToList()[0].SupplierValue);
    }

    [Fact]
    public async Task GetSingle_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetSupplier(It.IsAny<int>()))
            .ReturnsAsync(new Supplier { SupplierId = 1, SupplierName = "Supplier Name 1", SupplierDescription = "Supplier Description 1", SupplierValue = "EMIS" });

        var response = await _sut.Get(1);

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as Supplier;

        Assert.Equal("EMIS", value?.SupplierValue);
    }

    [Fact]
    public async Task Update_CallsValidator_WithExpectedParameters()
    {
        var expected = new SupplierUpdateRequest();
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<SupplierUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockValidator.Verify(v => v.IsValidUpdate(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_CallsValidator_WithExpectedParameters()
    {
        var expected = new SupplierDisableRequest();
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<SupplierDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);

        _mockValidator.Verify(v => v.IsValidDisable(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<SupplierUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = false, EntityFound = true });
        var response = await _sut.Put(It.IsAny<int>(), new SupplierUpdateRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<SupplierDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = false, EntityFound = true });
        var response = await _sut.Put(It.IsAny<int>(), new SupplierDisableRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new SupplierUpdateRequest();
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<SupplierUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.UpdateSupplier(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new SupplierDisableRequest();
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<SupplierDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.DisableSupplier(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<SupplierUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        _mockService.Setup(s => s.UpdateSupplier(It.IsAny<SupplierUpdateRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new SupplierUpdateRequest()));
    }

    [Fact]
    public async Task Disable_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<SupplierDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        _mockService.Setup(s => s.DisableSupplier(It.IsAny<SupplierDisableRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new SupplierDisableRequest()));
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<SupplierAddRequest>())).Returns(false);
        var response = await _sut.Post(new SupplierAddRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<SupplierAddRequest>())).Returns(true);
        var response = await _sut.Post(new SupplierAddRequest());
        Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<SupplierAddRequest>())).Returns(true);
        _mockService.Setup(s => s.AddSupplier(It.IsAny<SupplierAddRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Post(new SupplierAddRequest()));
    }

}