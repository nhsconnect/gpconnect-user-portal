using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Mappings;

public class UserMapTests
{
    private readonly UserMap _sut;

    public UserMapTests()
    {
        _sut = new UserMap();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }
}
