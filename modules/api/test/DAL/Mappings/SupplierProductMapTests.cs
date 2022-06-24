using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class SupplierProductMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new SupplierProductMap();
        Assert.NotNull(instance);
    }
}
