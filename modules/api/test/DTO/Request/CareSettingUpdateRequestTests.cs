using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class CareSettingUpdateRequestTests
{
    private readonly CareSettingUpdateRequest _sut;

    public CareSettingUpdateRequestTests()
    {
        _sut = new CareSettingUpdateRequest();
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
    public void PropertyCareSettingValue_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CareSettingValue = testValue;
        Assert.Equal(testValue, _sut.CareSettingValue);
    }
}
