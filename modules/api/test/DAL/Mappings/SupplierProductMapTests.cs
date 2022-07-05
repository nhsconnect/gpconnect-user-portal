using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class SupplierProductMapTests
{
    private readonly SupplierProductMap _sut;

    public SupplierProductMapTests()
    {
        _sut = new SupplierProductMap();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }
}
