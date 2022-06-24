using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class ProductMapTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new ProductMap();
        Assert.NotNull(instance);
    }
}
