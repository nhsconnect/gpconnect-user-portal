using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class SupplierTests
{
    private readonly Supplier _sut;

    public SupplierTests()
    {
        _sut = new Supplier();
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

    [Fact]
    public void CanSetAndGetSupplierName()
    {
        var testValue = "TestValue1";
        _sut.SupplierName = testValue;
        Assert.Equal(testValue, _sut.SupplierName);
    }

    [Fact]
    public void CanSetAndGetSupplierDescription()
    {
        var testValue = "TestValue1";
        _sut.SupplierDescription = testValue;
        Assert.Equal(testValue, _sut.SupplierDescription);
    }
}
