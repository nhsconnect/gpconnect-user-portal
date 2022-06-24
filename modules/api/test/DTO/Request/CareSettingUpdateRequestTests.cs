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
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetCareSettingId()
    {
        var testValue = 1;
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
}
