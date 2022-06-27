using GpConnect.NationalDataSharingPortal.Api.Dal.Constants;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Constants;

public class SiteInteractionTests
{
    private readonly SiteInteraction _sut;

    public SiteInteractionTests()
    {
        _sut = new SiteInteraction();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }
}
