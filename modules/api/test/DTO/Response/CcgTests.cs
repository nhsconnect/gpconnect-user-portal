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
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetCcgId()
    {
        var testValue = 1;
        _sut.CcgId = testValue;
        Assert.Equal(testValue, _sut.CcgId);
    }

    [Fact]
    public void CanSetAndGetCcgLinkedId()
    {
        var testValue = 1;
        _sut.CcgLinkedId = testValue;
        Assert.Equal(testValue, _sut.CcgLinkedId);
    }

    [Fact]
    public void CanSetAndGetCcgOdsCode()
    {
        var testValue = "TestValue1";
        _sut.CcgOdsCode = testValue;
        Assert.Equal(testValue, _sut.CcgOdsCode);
    }

    [Fact]
    public void CanSetAndGetCcgName()
    {
        var testValue = "TestValue1";
        _sut.CcgName = testValue;
        Assert.Equal(testValue, _sut.CcgName);
    }
}
