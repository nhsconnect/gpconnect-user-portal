using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
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
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullValidator_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SupplierController(default(ISupplierRequestValidator), _mockService.Object, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SupplierController(_mockValidator.Object, default(ISupplierService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SupplierController(_mockValidator.Object, _mockService.Object, default(ILogger<SupplierController>)));
    }

    [Fact]
    public async Task Get_WithNoParameters_ReturnsGivenType()
    {
        var result = await _sut.Get();
        Assert.IsType<ActionResult<IEnumerable<Supplier>>>(result);
    }

    [Fact]
    public async Task Get_WithSingleParameter_ReturnsGivenType()
    {
        var supplierId = 1;
        var result = await _sut.Get(supplierId);
        Assert.IsType<ActionResult<Supplier>>(result);
    }

    [Fact]
    public async Task Get_WithNullSingleParameter_DoesNotReturnAnException()
    {
        Assert.IsNotType<ArgumentNullException>(async () => await _sut.Get(default));
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
}
