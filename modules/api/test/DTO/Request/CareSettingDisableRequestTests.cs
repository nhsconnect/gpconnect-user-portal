using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class CareSettingDisableRequestTests
{
    private readonly CareSettingDisableRequest _sut;

    public CareSettingDisableRequestTests()
    {
        _sut = new CareSettingDisableRequest();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyCareSettingId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.CareSettingId = testValue;
        Assert.Equal(testValue, _sut.CareSettingId);
    }

    [Fact]
    public void PropertyCareSettingDisabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.CareSettingDisabled = testValue;
        Assert.Equal(testValue, _sut.CareSettingDisabled);
    }
}
