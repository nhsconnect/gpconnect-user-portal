using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class SupplierDisableRequestTests
{
    private readonly SupplierDisableRequest _sut;

    public SupplierDisableRequestTests()
    {
        _sut = new SupplierDisableRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetSupplierId()
    {
        var testValue = 1;
        _sut.SupplierId = testValue;
        Assert.Equal(testValue, _sut.SupplierId);
    }

    [Fact]
    public void CanSetAndGetSupplierDisabled()
    {
        var testValue = false;
        _sut.SupplierDisabled = testValue;
        Assert.Equal(testValue, _sut.SupplierDisabled);
    }
}
