using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class TransparencySiteRequestValidatorTest
{
    private readonly ITransparencySiteRequestValidator _sut;
    
    // setup
    public TransparencySiteRequestValidatorTest()
    {
        _sut = new TransparencySiteRequestValidator();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Call_IsValidId_ReturnsBoolean()
    {
        var id = Guid.NewGuid().ToString();
        var result = _sut.IsValidId(id);
        Assert.True(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Call_IsValidIdWithInvalidInput_ReturnsBoolean(string value)
    {
        var result = _sut.IsValidId(value);
        Assert.False(result);
    }

    [Fact]
    public void Call_IsValidRequestWithNullTransparencySiteRequest_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidRequest(default(TransparencySiteRequest)));
    }

    [Theory]
    [InlineData("Value", "", 1, 10)]
    [InlineData("", "Value", 1, 2)]
    public void IsValidRequest_GivenValidInput_ReturnsTrue(string providerCode, string providerName, int startPosition, int count)
    {
        var request = new TransparencySiteRequest {
            ProviderCode = providerCode,
            ProviderName = providerName,
            StartPosition = startPosition,
            Count = count
        };

        Assert.True(_sut.IsValidRequest(request));
    }

    [Theory]
    [InlineData("", "", 1, 2)]
    [InlineData("Value", "Value", 0, 2)]
    [InlineData("", "Value", -1, 2)]
    [InlineData("", "Value", 1, 0)]
    [InlineData("Value", "", -1, -1)]
    [InlineData("Value", "", 1, -1)]
    public void IsValidRequest_GivenInvalidInput_ReturnsFalse(string providerCode, string providerName, int startPosition, int count)
    {
        var request = new TransparencySiteRequest
        {
            ProviderCode = providerCode,
            ProviderName = providerName,
            StartPosition = startPosition,
            Count = count
        };

        Assert.False(_sut.IsValidRequest(request));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("not-a-guid")]
    public void IsValidId_GivenInvalidInput_ReturnsFalse(string input)
    {
        Assert.False(_sut.IsValidId(input));
    }

    [Fact]
    public void IsValidId_GivenValidInput_ReturnsTrue()
    {
        Assert.True(_sut.IsValidId("12341234-1234-1234-1234-123412341234"));
    }
}
