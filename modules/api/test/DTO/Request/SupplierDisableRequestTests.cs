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
    public void PropertySupplierDisabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.SupplierDisabled = testValue;
        Assert.Equal(testValue, _sut.SupplierDisabled);
    }
}
