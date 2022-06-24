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
    public void CanConstruct()
    {
        var instance = new BaseRequestValidator();
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanSetAndGetEntityFound()
    {
        var testValue = true;
        _sut.EntityFound = testValue;
        Assert.Equal(testValue, _sut.EntityFound);
    }

    [Fact]
    public void CanSetAndGetRequestValid()
    {
        var testValue = true;
        _sut.RequestValid = testValue;
        Assert.Equal(testValue, _sut.RequestValid);
    }
}
