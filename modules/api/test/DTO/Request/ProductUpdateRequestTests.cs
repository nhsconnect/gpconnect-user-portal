using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class ProductUpdateRequestTests
{
    private readonly ProductUpdateRequest _sut;

    public ProductUpdateRequestTests()
    {
        _sut = new ProductUpdateRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetProductId()
    {
        var testValue = 1;
        _sut.ProductId = testValue;
        Assert.Equal(testValue, _sut.ProductId);
    }

    [Fact]
    public void CanSetAndGetProductValue()
    {
        var testValue = "TestValue1";
        _sut.ProductValue = testValue;
        Assert.Equal(testValue, _sut.ProductValue);
    }
}
