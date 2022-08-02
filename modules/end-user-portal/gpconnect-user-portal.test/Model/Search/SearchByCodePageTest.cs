using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;

public class SearchByCodePageTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SearchByCodePageTest()
    {
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
    }

    [Fact]
    public void OnGet_ClearsProviderOdsCodeValidation_LeavesUserInput_ReturnsPage()
    {
        var validationKey = "ProviderOdsCode";
        var expectedOdsCode = "Test";

        var searchModel = new SearchByCodeModel(_mockOptions.Object)
        {
            ProviderOdsCode = expectedOdsCode
        };

        searchModel.ModelState.AddModelError(validationKey, "My Error");
        
        var result = searchModel.OnGet();
        
        Assert.StrictEqual(ModelValidationState.Unvalidated, searchModel.ModelState.GetFieldValidationState(validationKey));
        Assert.Equal(expectedOdsCode, searchModel.ProviderOdsCode);
        Assert.IsType<PageResult>(result);
    }

    [Theory]
    [InlineData("A20047")]
    [InlineData("A20047, B82132")]
    public void OnPost_IfValidSearchParameters_RedirectsToResultsPage(string providerOdsCode)
    {
        var searchModel = new SearchByCodeModel(_mockOptions.Object)
        {
            ProviderOdsCode = providerOdsCode
        };

        var result = searchModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Theory]
    [InlineData("$$$")]
    [InlineData("A_163763")]
    public void OnPost_IfInvalidModel_ReturnValidationError(string providerOdsCode)
    {
        var searchModel = new SearchByCodeModel(_mockOptions.Object)
        {
            ProviderOdsCode = providerOdsCode
        };

        searchModel.ModelState.AddModelError("ProviderOdsCode", "You must enter a value value for Provider Ods Code");

        var result = searchModel.OnPost();
        Assert.IsType<PageResult>(result);
        Assert.True(searchModel.ModelState.ErrorCount > 0);
    }
}
