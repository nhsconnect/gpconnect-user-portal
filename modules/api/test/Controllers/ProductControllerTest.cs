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

public class ProductControllerTest
{
    private readonly ProductController _sut;
    private readonly Mock<IProductRequestValidator> _mockValidator;
    private readonly Mock<IProductService> _mockService;
    private readonly Mock<ILogger<ProductController>> _mockLogger;

    // setup
    public ProductControllerTest()
    {
        _mockValidator = new Mock<IProductRequestValidator>();
        _mockService = new Mock<IProductService>();
        _mockLogger = new Mock<ILogger<ProductController>>();

         _sut = new ProductController(_mockValidator.Object, _mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullValidator_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductController(default(IProductRequestValidator), _mockService.Object, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductController(_mockValidator.Object, default(IProductService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductController(_mockValidator.Object, _mockService.Object, default(ILogger<ProductController>)));
    }

    [Fact]
    public async Task Get_WithNoParameters_ReturnsGivenType()
    {
        var result = await _sut.Get();
        Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
    }

    [Fact]
    public async Task Get_WithSingleParameter_ReturnsGivenType()
    {
        var productId = 1;
        var result = await _sut.Get(productId);
        Assert.IsType<ActionResult<Product>>(result);
    }

    [Fact]
    public async Task Get_WithNullSingleParameter_DoesNotReturnAnException()
    {
        Assert.IsNotType<ArgumentNullException>(async () => await _sut.Get(default));
    }

    [Fact]
    public async Task GetList_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product Name 1", ProductDescription = "Product Description 1", ProductValue = "EMIS Symphony" },
                new Product { ProductId = 2, ProductName = "Product Name 2", ProductDescription = "Product Description 2", ProductValue = "SystemOne" }
            });

        var response = await _sut.Get();

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<Product>;

        Assert.StrictEqual(2, value?.ToList().Count);
        Assert.Equal("EMIS Symphony", value?.ToList()[0].ProductValue);
    }

    [Fact]
    public async Task GetSingle_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetProduct(It.IsAny<int>()))
            .ReturnsAsync(new Product { ProductId = 2, ProductName = "Product Name 2", ProductDescription = "Product Description 2", ProductValue = "SystemOne" });

        var response = await _sut.Get(1);

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as Product;

        Assert.Equal("SystemOne", value?.ProductValue);
    }

}
