using Xunit;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class TransparencySitesTests
{
    private readonly TransparencySites _sut;

    public TransparencySitesTests()
    {
        _sut = new TransparencySites();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        var instance = new TransparencySites();
        Assert.NotNull(instance);
    }

    [Fact]
    public void PropertyTotalResults_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.TotalResults = testValue;
        Assert.Equal(testValue, _sut.TotalResults);
    }

    [Fact]
    public void PropertyResults_WithProvidedValues_CanSetAndGet()
    {
        var testValue = default(List<TransparencySite>);
        _sut.Results = testValue;
        Assert.Equal(testValue, _sut.Results);
    }
}
