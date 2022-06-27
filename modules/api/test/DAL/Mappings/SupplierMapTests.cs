using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class SupplierMapTests
{
    private readonly SupplierMap _sut;

    public SupplierMapTests()
    {
        _sut = new SupplierMap();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }
}
