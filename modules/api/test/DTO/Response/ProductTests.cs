using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class ProductTests
{
    private readonly Product _sut;

    public ProductTests()
    {
        _sut = new Product();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetProductId()
    {
        var testValue = 1621644064;
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

    [Fact]
    public void CanSetAndGetProductName()
    {
        var testValue = "TestValue1";
        _sut.ProductName = testValue;
        Assert.Equal(testValue, _sut.ProductName);
    }

    [Fact]
    public void CanSetAndGetProductDescription()
    {
        var testValue = "TestValue1";
        _sut.ProductDescription = testValue;
        Assert.Equal(testValue, _sut.ProductDescription);
    }
}
