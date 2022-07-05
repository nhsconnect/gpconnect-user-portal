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
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyProductId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1621644064;
        _sut.ProductId = testValue;
        Assert.Equal(testValue, _sut.ProductId);
    }

    [Fact]
    public void PropertyProductValue_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.ProductValue = testValue;
        Assert.Equal(testValue, _sut.ProductValue);
    }

    [Fact]
    public void PropertyProductName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.ProductName = testValue;
        Assert.Equal(testValue, _sut.ProductName);
    }

    [Fact]
    public void PropertyProductDescription_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.ProductDescription = testValue;
        Assert.Equal(testValue, _sut.ProductDescription);
    }
}
