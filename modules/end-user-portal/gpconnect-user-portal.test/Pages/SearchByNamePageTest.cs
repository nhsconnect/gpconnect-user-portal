using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;

public class SearchByNamePageTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SearchByNamePageTest()
    {
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
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

        var result = searchModel.OnPostSearchAsync();        
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

        var result = searchModel.OnPostSearchAsync();
        Assert.IsType<PageResult>(result);
        Assert.True(searchModel.ModelState.ErrorCount > 0);
    }
}
