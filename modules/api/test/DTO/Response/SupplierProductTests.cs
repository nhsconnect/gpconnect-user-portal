using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class SupplierProductTests
{
    private readonly SupplierProduct _sut;

    public SupplierProductTests()
    {
        _sut = new SupplierProduct();
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
    public void CanSetAndGetSupplierProductId()
    {
        var testValue = 1;
        _sut.SupplierProductId = testValue;
        Assert.Equal(testValue, _sut.SupplierProductId);
    }

    [Fact]
    public void CanSetAndGetSupplierName()
    {
        var testValue = "TestValue1";
        _sut.SupplierName = testValue;
        Assert.Equal(testValue, _sut.SupplierName);
    }

    [Fact]
    public void CanSetAndGetSupplierProductName()
    {
        var testValue = "TestValue1";
        _sut.SupplierProductName = testValue;
        Assert.Equal(testValue, _sut.SupplierProductName);
    }
}
