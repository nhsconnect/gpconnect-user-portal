using GpConnect.NationalDataSharingPortal.Api.Validators;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class BaseRequestValidatorTests
{
    private readonly BaseRequestValidator _sut;

    public BaseRequestValidatorTests()
    {
        _sut = new BaseRequestValidator();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyEntityFound_WithProvidedValues_CanSetAndGet()
    {
        var testValue = true;
        _sut.EntityFound = testValue;
        Assert.Equal(testValue, _sut.EntityFound);
    }

    [Fact]
    public void PropertyRequestValid_WithProvidedValues_CanSetAndGet()
    {
        var testValue = true;
        _sut.RequestValid = testValue;
        Assert.Equal(testValue, _sut.RequestValid);
    }
}
