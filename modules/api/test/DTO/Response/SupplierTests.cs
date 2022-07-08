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
    public void PropertySupplierValue_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.SupplierValue = testValue;
        Assert.Equal(testValue, _sut.SupplierValue);
    }

    [Fact]
    public void PropertySupplierName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.SupplierName = testValue;
        Assert.Equal(testValue, _sut.SupplierName);
    }

    [Fact]
    public void PropertySupplierDescription_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.SupplierDescription = testValue;
        Assert.Equal(testValue, _sut.SupplierDescription);
    }
}
