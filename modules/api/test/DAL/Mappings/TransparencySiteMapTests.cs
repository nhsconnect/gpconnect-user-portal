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
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }
}
