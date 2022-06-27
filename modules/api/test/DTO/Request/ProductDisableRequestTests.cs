using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class ProductDisableRequestTests
{
    private readonly ProductDisableRequest _sut;

    public ProductDisableRequestTests()
    {
        _sut = new ProductDisableRequest();
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
    public void CanSetAndGetProductDisabled()
    {
        var testValue = true;
        _sut.ProductDisabled = testValue;
        Assert.Equal(testValue, _sut.ProductDisabled);
    }
}
