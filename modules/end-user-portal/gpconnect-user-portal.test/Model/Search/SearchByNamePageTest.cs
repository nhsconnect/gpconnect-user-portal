using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;

public class FeedbackPageTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public FeedbackPageTest()
    {
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
    }

    [Fact]
    public void OnGet_ClearsProviderNameValidation_LeavesUserInput_ReturnsPage()
    {
        var validationKey = "ProviderName";
        var expectedName = "Test";

        var searchModel = new SearchByNameModel(_mockOptions.Object)
        {
            ProviderName = expectedName
        };

        searchModel.ModelState.AddModelError(validationKey, "My Error");
        
        var result = searchModel.OnGet();
        
        Assert.StrictEqual(ModelValidationState.Unvalidated, searchModel.ModelState.GetFieldValidationState(validationKey));
        Assert.Equal(expectedName, searchModel.ProviderName);
        Assert.IsType<PageResult>(result);
    }

    [Theory]
    [InlineData("Medical")]
    [InlineData("Practice, Surgery")]
    public void OnPost_IfValidSearchParameters_RedirectsToResultsPage(string providerName)
    {
        var searchModel = new SearchByNameModel(_mockOptions.Object)
        {
            ProviderName = providerName
        };

        var result = searchModel.OnPost();        
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void OnPost_IfInvalidModel_ReturnValidationError(string providerName)
    {
        var searchModel = new SearchByNameModel(_mockOptions.Object)
        {
            ProviderName = providerName
        };

        searchModel.ModelState.AddModelError("ProviderName", "You must enter a value for Provider Name");

        var result = searchModel.OnPost();
        Assert.IsType<PageResult>(result);
        Assert.True(searchModel.ModelState.ErrorCount > 0);
    }
}
