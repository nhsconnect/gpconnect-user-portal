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
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertySupplierId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.SupplierId = testValue;
        Assert.Equal(testValue, _sut.SupplierId);
    }

    [Fact]
    public void PropertySupplierProductId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.SupplierProductId = testValue;
        Assert.Equal(testValue, _sut.SupplierProductId);
    }

    [Fact]
    public void PropertySupplierName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.SupplierName = testValue;
        Assert.Equal(testValue, _sut.SupplierName);
    }

    [Fact]
    public void PropertySupplierProductName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.SupplierProductName = testValue;
        Assert.Equal(testValue, _sut.SupplierProductName);
    }
}
