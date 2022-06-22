using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;

public class SearchByCodePageTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SearchByCodePageTest()
    {
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
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

        var result = searchModel.OnPostSearchAsync();
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

        var result = searchModel.OnPostSearchAsync();
        Assert.IsType<PageResult>(result);
        Assert.True(searchModel.ModelState.ErrorCount > 0);
    }
}
