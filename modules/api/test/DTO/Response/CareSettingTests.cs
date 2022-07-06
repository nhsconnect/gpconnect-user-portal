using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class CareSettingTests
{
    private readonly CareSetting _sut;

    public CareSettingTests()
    {
        _sut = new CareSetting();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyCareSettingId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1111784144;
        _sut.CareSettingId = testValue;
        Assert.Equal(testValue, _sut.CareSettingId);
    }

    [Fact]
    public void PropertyCareSettingValue_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CareSettingValue = testValue;
        Assert.Equal(testValue, _sut.CareSettingValue);
    }

    [Fact]
    public void PropertyCareSettingName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CareSettingName = testValue;
        Assert.Equal(testValue, _sut.CareSettingName);
    }

    [Fact]
    public void PropertyCareSettingDescription_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CareSettingDescription = testValue;
        Assert.Equal(testValue, _sut.CareSettingDescription);
    }
}
