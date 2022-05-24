using gpconnect_user_portal.api.dto.request;
using gpconnect_user_portal.api.validators;
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
    [InlineData("Value", "", "", "")]
    [InlineData("", "Value", "", "")]
    [InlineData("", "", "Value", "")]
    [InlineData("", "", "", "Value")]
    public void IsValid_GivenValidInput_ReturnsTrue(string providerCode, string providerName, string ccgName, string ccgCode)
    {
        var request = new TransparencySiteRequest {
            ProviderCode = providerCode,
            ProviderName = providerName,
            CcgCode = ccgCode,
            CcgName = ccgName
        };

        Assert.True(_sut.IsValid(request));
    }

    [Fact]
    public void IsValid_GivenInvalidInput_ReturnsFalse()
    {
        Assert.False(_sut.IsValid(new TransparencySiteRequest()));
    }
}
