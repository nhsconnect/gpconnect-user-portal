using GpConnect.NationalDataSharingPortal.Api.Validators;
using System;
using Xunit;
using Moq;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class ProductRequestValidatorTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductRequestValidator _sut;

    public ProductRequestValidatorTests()
    {
        _mockService = new Mock<IProductService>();
        _sut = new ProductRequestValidator(_mockService.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullProductService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductRequestValidator(default(IProductService)));
    }

    [Fact]
    public async Task Call_IsValidUpdate_ReturnsBoolean()
    {
        var request = new ProductUpdateRequest { ProductId = 1, ProductValue = "TestValue1" };
        var result = await _sut.IsValidUpdate(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task Call_IsValidUpdateWithNullProductUpdateRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidUpdate(default(ProductUpdateRequest)));
    }

    [Fact]
    public async Task Call_IsValidDisable_ReturnsBoolean()
    {
        var request = new ProductDisableRequest { ProductId = 1, ProductDisabled = true };
        var result = await _sut.IsValidDisable(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task Call_IsValidDisableWithNullProductDisableRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidDisable(default(ProductDisableRequest)));
    }

    [Fact]
    public void Call_IsValidAdd_ReturnsBoolean()
    {
        var request = new ProductAddRequest { ProductValue = "TestValue1" };
        var result = _sut.IsValidAdd(request);
        Assert.True(result);
    }

    [Fact]
    public void Call_IsValidAddWithNullProductAddRequest_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidAdd(default(ProductAddRequest)));
    }
}
