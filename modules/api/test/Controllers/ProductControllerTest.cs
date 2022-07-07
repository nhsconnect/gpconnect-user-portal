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
    public async Task Put_WithIdAndProductUpdateRequest_ReturnsGivenType()
    {
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<ProductUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        var id = 333400887;
        var productUpdateRequest = new ProductUpdateRequest { ProductId = 1, ProductValue = "TestValue1" };
        var result = await _sut.Put(id, productUpdateRequest);
        Assert.IsType<ActionResult<ProductUpdateRequest>>(result);
    }

    [Fact]
    public async Task Put_WithNullIdAndProductUpdateRequest_ThrowsNullReferenceException()
    {
        var productUpdateRequest = new ProductUpdateRequest { ProductId = 1, ProductValue = "TestValue1" };
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _sut.Put(default, productUpdateRequest));
    }

    [Fact]
    public async Task Put_WithNullIdAndNullProductUpdateRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _sut.Put(default(int), default(ProductUpdateRequest)));
    }

    [Fact]
    public async Task Put_WithIdAndProductDisableRequest_ReturnsGivenType()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<ProductDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        var id = 1419042889;
        var productDisableRequest = new ProductDisableRequest { ProductId = 1, ProductDisabled = false };
        var result = await _sut.Put(id, productDisableRequest);
        Assert.IsType<ActionResult<ProductDisableRequest>>(result);
    }

    [Fact]
    public async Task Put_WithNullIdAndProductDisableRequest_ThrowsNullReferenceException()
    {
        var productDisableRequest = new ProductDisableRequest { ProductId = 1, ProductDisabled = false };
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _sut.Put(default, productDisableRequest));
    }

    [Fact]
    public async Task Put_WithNullIdAndNullProductDisableRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _sut.Put(default(int), default(ProductDisableRequest)));
    }

    [Fact]
    public async Task Post_WithValidParameters_ReturnsActionResult()
    {
        var productAddRequest = new ProductAddRequest { ProductValue = "TestValue1" };
        var result = await _sut.Post(productAddRequest);
        Assert.IsType<ActionResult<SupplierProduct>>(result);
    }

    [Fact]
    public async Task Post_WithNullProductAddRequest_ThrowsArgumentNullException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.Post(default(ProductAddRequest)));
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

    [Fact]
    public async Task Update_CallsValidator_WithExpectedParameters()
    {
        var expected = new ProductUpdateRequest();
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<ProductUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockValidator.Verify(v => v.IsValidUpdate(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_CallsValidator_WithExpectedParameters()
    {
        var expected = new ProductDisableRequest();
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<ProductDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);

        _mockValidator.Verify(v => v.IsValidDisable(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<ProductUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = false, EntityFound = true });
        var response = await _sut.Put(It.IsAny<int>(), new ProductUpdateRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<ProductDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = false, EntityFound = true });
        var response = await _sut.Put(It.IsAny<int>(), new ProductDisableRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Update_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new ProductUpdateRequest();
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<ProductUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.UpdateProduct(expected), Times.Once);
    }

    [Fact]
    public async Task Disable_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        var expected = new ProductDisableRequest();
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<ProductDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });

        await _sut.Put(It.IsAny<int>(), expected);
        _mockService.Verify(s => s.DisableProduct(expected), Times.Once);
    }

    [Fact]
    public async Task Update_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidUpdate(It.IsAny<ProductUpdateRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        _mockService.Setup(s => s.UpdateProduct(It.IsAny<ProductUpdateRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new ProductUpdateRequest()));
    }

    [Fact]
    public async Task Disable_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidDisable(It.IsAny<ProductDisableRequest>())).ReturnsAsync(new BaseRequestValidator() { RequestValid = true, EntityFound = true });
        _mockService.Setup(s => s.DisableProduct(It.IsAny<ProductDisableRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Put(It.IsAny<int>(), new ProductDisableRequest()));
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<ProductAddRequest>())).Returns(false);
        var response = await _sut.Post(new ProductAddRequest());
        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<ProductAddRequest>())).Returns(true);
        var response = await _sut.Post(new ProductAddRequest());
        Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Add_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<ProductAddRequest>())).Returns(true);
        _mockService.Setup(s => s.AddProduct(It.IsAny<ProductAddRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.Post(new ProductAddRequest()));
    }

}
