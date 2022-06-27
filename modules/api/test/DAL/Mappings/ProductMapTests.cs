using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class ProductMapTests
{
    private readonly ProductMap _sut;

    public ProductMapTests()
    {
        _sut = new ProductMap();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }
}
