using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class TransparencySiteMapTests
{
    private readonly TransparencySiteMap _sut;

    public TransparencySiteMapTests()
    {
        _sut = new TransparencySiteMap();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }
}
