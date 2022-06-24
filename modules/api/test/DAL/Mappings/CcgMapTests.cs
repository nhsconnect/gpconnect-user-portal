using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class CcgMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new CcgMap();
        Assert.NotNull(instance);
    }
}
