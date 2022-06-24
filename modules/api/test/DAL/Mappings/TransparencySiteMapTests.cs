using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class TransparencySiteMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new TransparencySiteMap();
        Assert.NotNull(instance);
    }
}
