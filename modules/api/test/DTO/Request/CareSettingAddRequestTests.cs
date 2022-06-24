using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class CareSettingAddRequestTests
{
    private readonly CareSettingAddRequest _sut;

    public CareSettingAddRequestTests()
    {
        _sut = new CareSettingAddRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetCareSettingValue()
    {
        var testValue = "TestValue1";
        _sut.CareSettingValue = testValue;
        Assert.Equal(testValue, _sut.CareSettingValue);
    }
}
