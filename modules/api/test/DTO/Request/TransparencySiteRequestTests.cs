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
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyProviderCode_WithProvidedValues_CanSetAndGet()
    {
        var testValue = default(string?);
        _sut.ProviderCode = testValue;
        Assert.Equal(testValue, _sut.ProviderCode);
    }

    [Fact]
    public void PropertyProviderName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = default(string?);
        _sut.ProviderName = testValue;
        Assert.Equal(testValue, _sut.ProviderName);
    }

    [Fact]
    public void PropertyStartPosition_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 92608808;
        _sut.StartPosition = testValue;
        Assert.Equal(testValue, _sut.StartPosition);
    }

    [Fact]
    public void PropertyCount_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1042382841;
        _sut.Count = testValue;
        Assert.Equal(testValue, _sut.Count);
    }
}
