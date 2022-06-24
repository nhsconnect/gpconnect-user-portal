using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class ProductAddRequestTests
{
    private ProductAddRequest _sut;

    public ProductAddRequestTests()
    {
        _sut = new ProductAddRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetProductValue()
    {
        var testValue = "TestValue1";
        _sut.ProductValue = testValue;
        Assert.Equal(testValue, _sut.ProductValue);
    }
}
