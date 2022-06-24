using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class SupplierAddRequestTests
{
    private readonly SupplierAddRequest _sut;

    public SupplierAddRequestTests()
    {
        _sut = new SupplierAddRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetSupplierValue()
    {
        var testValue = "TestValue1";
        _sut.SupplierValue = testValue;
        Assert.Equal(testValue, _sut.SupplierValue);
    }
}
