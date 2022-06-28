using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Model;

public class BaseModelTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockConfig;
    private readonly ApplicationParameters configObject;

    public BaseModelTest()
    {
        configObject = new ApplicationParameters();

        _mockConfig = new Mock<IOptions<ApplicationParameters>>();
        _mockConfig.SetupGet(c => c.Value).Returns(configObject);
    }

    [Fact]
    public void ProductName_RetrievesValueFromConfig_Returns()
    {
        const string ExpectedProductName = "MyProduct";
        
        configObject.ProductName = ExpectedProductName;

        var baseModel = new BaseModel(_mockConfig.Object);
        var result = baseModel.ProductName;

        _mockConfig.Verify(c => c.Value, Times.Once);

        Assert.Equal(ExpectedProductName, result);
    }

    [Fact]
    public void ProductVersion_RetrievesValueFromConfig_Returns()
    {
        const string ExpectedProductVersion = "MyVersion";
        
        configObject.ProductVersion = ExpectedProductVersion;

        var baseModel = new BaseModel(_mockConfig.Object);
        var result = baseModel.ProductVersion;

        _mockConfig.Verify(c => c.Value, Times.Once);

        Assert.Equal(ExpectedProductVersion, result);
    }

    [Fact]
    public void OwnerEmailAddress_RetrievesValueFromConfig_Returns()
    {
        const string ExpectedOwnerEmailAddress = "MyEmailAddress";
        
        configObject.OwnerEmailAddress = ExpectedOwnerEmailAddress;

        var baseModel = new BaseModel(_mockConfig.Object);
        var result = baseModel.OwnerEmailAddress;

        _mockConfig.Verify(c => c.Value, Times.Once);

        Assert.Equal(ExpectedOwnerEmailAddress, result);
    }

    [Fact]
    public void OwnerTelephone_RetrievesValueFromConfig_Returns()
    {
        const string ExpectedOwnerTelephone = "0123456789";
        
        configObject.OwnerTelephone = ExpectedOwnerTelephone;

        var baseModel = new BaseModel(_mockConfig.Object);
        var result = baseModel.OwnerTelephone;

        _mockConfig.Verify(c => c.Value, Times.Once);

        Assert.Equal(ExpectedOwnerTelephone, result);
    }
}