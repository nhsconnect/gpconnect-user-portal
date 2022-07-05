using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class ProductServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly ProductService _sut;

    public ProductServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new ProductService(_mockDataService.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullDataService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductService(default(IDataService)));
    }

    [Fact]
    public async Task Get_Products_ReturnsProducts()
    {
        var list = Task.FromResult(new List<Product>());
        _mockDataService.Setup(d => d.ExecuteQuery<Product>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var result = await _sut.GetProducts();
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
    }

    [Fact]
    public async Task Get_Product_ReturnsProduct()
    {
        var single = Task.FromResult(new Product());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<Product>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var id = 1;
        var result = await _sut.GetProduct(id);
        Assert.IsAssignableFrom<Product>(result);
    }

    [Fact]
    public async Task Call_ProductAddRequest_ReturnsProduct()
    {
        var single = Task.FromResult(new Product());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<Product>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var productAddRequest = new ProductAddRequest { ProductValue = "TestValue1" };
        var result = await _sut.AddProduct(productAddRequest);
        Assert.IsAssignableFrom<Product>(result);
    }

    [Fact]
    public async Task Call_WithNullProductAddRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.AddProduct(default(ProductAddRequest)));
    }

    [Fact]
    public async Task Call_WithNullProductDisableRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.DisableProduct(default(ProductDisableRequest)));
    }

    [Fact]
    public async Task Call_WithNullProductUpdateRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.UpdateProduct(default(ProductUpdateRequest)));
    }
}
