using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Xunit;

namespace gpconnect_user_portal.api.test.validators;

public class TransparencySiteRequestValidatorTest
{
    private readonly ITransparencySiteRequestValidator _sut;
    
    // setup
    public TransparencySiteRequestValidatorTest()
    {
        _sut = new TransparencySiteRequestValidator();
    }

    [Theory]
    [InlineData("Value", "")]
    [InlineData("", "Value")]
    public void IsValidRequest_GivenValidInput_ReturnsTrue(string providerCode, string providerName)
    {
        var request = new TransparencySiteRequest {
            ProviderCode = providerCode,
            ProviderName = providerName
        };

        Assert.True(_sut.IsValidRequest(request));
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("Value", "Value")]
    public void IsValidRequest_GivenInvalidInput_ReturnsFalse(string providerCode, string providerName)
    {
        Assert.False(_sut.IsValidRequest(new TransparencySiteRequest
        {
            ProviderCode = providerCode,
            ProviderName = providerName
        }));
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
