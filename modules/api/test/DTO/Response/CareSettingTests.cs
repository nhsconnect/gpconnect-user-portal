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
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetCareSettingId()
    {
        var testValue = 1111784144;
        _sut.CareSettingId = testValue;
        Assert.Equal(testValue, _sut.CareSettingId);
    }

    [Fact]
    public void CanSetAndGetCareSettingValue()
    {
        var testValue = "TestValue1";
        _sut.CareSettingValue = testValue;
        Assert.Equal(testValue, _sut.CareSettingValue);
    }

    [Fact]
    public void CanSetAndGetCareSettingName()
    {
        var testValue = "TestValue1";
        _sut.CareSettingName = testValue;
        Assert.Equal(testValue, _sut.CareSettingName);
    }

    [Fact]
    public void CanSetAndGetCareSettingDescription()
    {
        var testValue = "TestValue1";
        _sut.CareSettingDescription = testValue;
        Assert.Equal(testValue, _sut.CareSettingDescription);
    }
}
