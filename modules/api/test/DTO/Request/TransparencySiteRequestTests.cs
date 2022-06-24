using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Request;

public class TransparencySiteRequestTests
{
    private readonly TransparencySiteRequest _sut;

    public TransparencySiteRequestTests()
    {
        _sut = new TransparencySiteRequest();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetProviderCode()
    {
        var testValue = default(string?);
        _sut.ProviderCode = testValue;
        Assert.Equal(testValue, _sut.ProviderCode);
    }

    [Fact]
    public void CanSetAndGetProviderName()
    {
        var testValue = default(string?);
        _sut.ProviderName = testValue;
        Assert.Equal(testValue, _sut.ProviderName);
    }

    [Fact]
    public void CanSetAndGetStartPosition()
    {
        var testValue = 92608808;
        _sut.StartPosition = testValue;
        Assert.Equal(testValue, _sut.StartPosition);
    }

    [Fact]
    public void CanSetAndGetCount()
    {
        var testValue = 1042382841;
        _sut.Count = testValue;
        Assert.Equal(testValue, _sut.Count);
    }
}
