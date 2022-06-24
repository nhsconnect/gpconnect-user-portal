using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class CareSettingMapTests
{    
    [Fact]
    public void CanConstruct()
    {
        var instance = new CareSettingMap();
        Assert.NotNull(instance);
    }
}
