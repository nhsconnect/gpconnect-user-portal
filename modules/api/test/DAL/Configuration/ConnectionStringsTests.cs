using GpConnect.NationalDataSharingPortal.Api.Dal.Configuration;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Configuration;

public class ConnectionStringsTests
{
    private readonly ConnectionStrings _sut;

    public ConnectionStringsTests()
    {
        _sut = new ConnectionStrings();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGet_PropertyDefaultConnection_WithProvidedValues()
    {
        var testValue = "DatabaseConnectionString";
        _sut.DefaultConnection = testValue;
        Assert.Equal(testValue, _sut.DefaultConnection);
    }
}
