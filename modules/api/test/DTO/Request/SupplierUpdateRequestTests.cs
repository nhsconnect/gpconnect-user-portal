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
}
