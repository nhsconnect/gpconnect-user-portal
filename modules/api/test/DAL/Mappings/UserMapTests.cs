using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class UserMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new UserMap();
        Assert.NotNull(instance);
    }
}
