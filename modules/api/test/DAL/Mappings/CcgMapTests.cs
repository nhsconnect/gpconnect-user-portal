using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class CcgMapTests
{
    private readonly CcgMap _sut;

    public CcgMapTests()
    {
        _sut = new CcgMap();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }
}
