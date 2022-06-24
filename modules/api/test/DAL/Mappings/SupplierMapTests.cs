using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class SupplierMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new SupplierMap();
        Assert.NotNull(instance);
    }
}
