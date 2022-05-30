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
    public void IsValid_GivenValidInput_ReturnsTrue(string providerCode, string providerName)
    {
        var request = new TransparencySiteRequest {
            ProviderCode = providerCode,
            ProviderName = providerName
        };

        Assert.True(_sut.IsValid(request));
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("Value", "Value")]
    public void IsValid_GivenInvalidInput_ReturnsFalse(string providerCode, string providerName)
    {
        Assert.False(_sut.IsValid(new TransparencySiteRequest
        {
            ProviderCode = providerCode,
            ProviderName = providerName
        }));
    }
}
