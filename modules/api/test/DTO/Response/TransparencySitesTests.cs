using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class TransparencySitesTests
{
    private TransparencySites _sut;

    public TransparencySitesTests()
    {
        _sut = new TransparencySites();
    }

    [Fact]
    public void CanConstruct()
    {
        var instance = new TransparencySites();
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanSetAndGetTotalResults()
    {
        var testValue = 1;
        _sut.TotalResults = testValue;
        Assert.Equal(testValue, _sut.TotalResults);
    }

    [Fact]
    public void CanSetAndGetResults()
    {
        var testValue = default(List<TransparencySite>);
        _sut.Results = testValue;
        Assert.Equal(testValue, _sut.Results);
    }
}
