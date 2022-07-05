using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class CcgTests
{
    private readonly Ccg _sut;

    public CcgTests()
    {
        _sut = new Ccg();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyCcgId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.CcgId = testValue;
        Assert.Equal(testValue, _sut.CcgId);
    }

    [Fact]
    public void PropertyCcgLinkedId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.CcgLinkedId = testValue;
        Assert.Equal(testValue, _sut.CcgLinkedId);
    }

    [Fact]
    public void PropertyCcgOdsCode_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CcgOdsCode = testValue;
        Assert.Equal(testValue, _sut.CcgOdsCode);
    }

    [Fact]
    public void PropertyCcgName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CcgName = testValue;
        Assert.Equal(testValue, _sut.CcgName);
    }
}
