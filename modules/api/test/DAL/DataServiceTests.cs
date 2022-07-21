using Amazon;
using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using T = System.String;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal;

public class DataServiceTests
{
    private readonly DataService _sut;
    private readonly Mock<IOptions<ConnectionStrings>> _mockOptionsAccessor;
    private readonly Mock<ILogger<DataService>> _mockLogger;

    public DataServiceTests()
    {
        _mockOptionsAccessor = new Mock<IOptions<ConnectionStrings>>();
        _mockOptionsAccessor.Setup(o => o.Value).Returns(new ConnectionStrings() { DefaultConnection = "Test" });
        _mockLogger = new Mock<ILogger<DataService>>();
        _sut = new DataService(_mockOptionsAccessor.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullOptionsAccessor_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new DataService(default(IOptions<ConnectionStrings>), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithRdsToken_UsesRdsApi()
    {

        var tokenMock = new Mock<IAuthTokenGenerator>();

        tokenMock.Setup(m => m.GenerateAuthToken(RegionEndpoint.EUWest2, "test-db-server", 5432, "test-user")).Returns("very secret password");
        var rdsOptions = new Mock<IOptions<ConnectionStrings>>();

        rdsOptions.Setup(o => o.Value).Returns(new ConnectionStrings() { DefaultConnection = "Host=test-db-server;User=test-user;Password=${rdsToken};"});

        var service = new DataService(rdsOptions.Object, _mockLogger.Object, tokenMock.Object);

        var connectionString = service.ConnectionString;

        Assert.Contains("very secret password", connectionString);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new DataService(_mockOptionsAccessor.Object, default(ILogger<DataService>)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CallExecuteQuery_WithTAndStringAndNullableDynamicParameters_ThrowsArgumentNullException(string value)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.ExecuteQuery<T>(value, default(DynamicParameters?)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CallExecuteQuery_WithStringAndNullDynamicParameters_ThrowsArgumentNullException(string value)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.ExecuteQuery(value, default(DynamicParameters)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CallExecuteQueryFirstOrDefault_WithTAndStringAndNullableDynamicParameters_ThrowsArgumentNullException(string value)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.ExecuteQueryFirstOrDefault<T>(value, default(DynamicParameters?)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CallExecuteScalar_WithStringAndNullDynamicParameters_ThrowsArgumentNullException(string value)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.ExecuteScalar(value, default(DynamicParameters)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CallExecuteTextQuery_WithTAndString_ThrowsArgumentNullException(string value)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.ExecuteTextQuery<T>(value));
    }
}
