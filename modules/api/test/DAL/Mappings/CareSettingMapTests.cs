using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class CareSettingMapTests
{
    private readonly CareSettingMap _sut;

    public CareSettingMapTests()
    {
        _sut = new CareSettingMap();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }
}
