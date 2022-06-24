using GpConnect.NationalDataSharingPortal.Api.Dal;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal;

public class ConnectionStringsTests
{
    private ConnectionStrings _sut;

    public ConnectionStringsTests()
    {
        _sut = new ConnectionStrings();
    }

    [Fact]
    public void CanConstruct()
    {
        var instance = new ConnectionStrings();
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanSetAndGetDefaultConnection()
    {
        var testValue = "DatabaseConnectionString";
        _sut.DefaultConnection = testValue;
        Assert.Equal(testValue, _sut.DefaultConnection);
    }
}
