using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class ProductAddRequestTests
{
    private readonly ProductAddRequest _sut;

    public ProductAddRequestTests()
    {
        _sut = new ProductAddRequest();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyProductValue_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.ProductValue = testValue;
        Assert.Equal(testValue, _sut.ProductValue);
    }
}
