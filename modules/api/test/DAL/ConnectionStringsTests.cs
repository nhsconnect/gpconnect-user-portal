using GpConnect.NationalDataSharingPortal.Api.Dal;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal;

public class ConnectionStringsTests
{
    private readonly ConnectionStrings _sut;

    public ConnectionStringsTests()
    {
        _sut = new ConnectionStrings();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetDefaultConnection()
    {
        var testValue = "DatabaseConnectionString";
        _sut.DefaultConnection = testValue;
        Assert.Equal(testValue, _sut.DefaultConnection);
    }
}
