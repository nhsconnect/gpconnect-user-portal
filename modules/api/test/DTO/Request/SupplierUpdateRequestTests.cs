using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class SupplierUpdateRequestTests
{
    private readonly SupplierUpdateRequest _sut;

    public SupplierUpdateRequestTests()
    {
        _sut = new SupplierUpdateRequest();
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
    public void CanSetAndGetSupplierValue()
    {
        var testValue = "TestValue1";
        _sut.SupplierValue = testValue;
        Assert.Equal(testValue, _sut.SupplierValue);
    }
}
