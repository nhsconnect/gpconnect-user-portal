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
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CannotConstructWithNullProductService()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductRequestValidator(default(IProductService)));
    }

    [Fact]
    public async Task CanCallIsValidUpdate()
    {
        var request = new ProductUpdateRequest { ProductId = 1, ProductValue = "TestValue1" };
        var result = await _sut.IsValidUpdate(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task CannotCallIsValidUpdateWithNullRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidUpdate(default(ProductUpdateRequest)));
    }

    [Fact]
    public async Task CanCallIsValidDisable()
    {
        var request = new ProductDisableRequest { ProductId = 1, ProductDisabled = true };
        var result = await _sut.IsValidDisable(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task CannotCallIsValidDisableWithNullRequest()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidDisable(default(ProductDisableRequest)));
    }

    [Fact]
    public void CanCallIsValidAdd()
    {
        var request = new ProductAddRequest { ProductValue = "TestValue1" };
        var result = _sut.IsValidAdd(request);
        Assert.True(result);
    }

    [Fact]
    public void CannotCallIsValidAddWithNullRequest()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidAdd(default(ProductAddRequest)));
    }
}
