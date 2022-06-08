using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test
{
  public class SearchPageTests
  {
    private readonly Mock<IOptions<Core.ApplicationParameters>> _mockOptions;
    private readonly Mock<ISiteService> _mockSiteService;

    public SearchPageTests()
    {
      _mockOptions = new Mock<IOptions<Core.ApplicationParameters>>();
      _mockSiteService = new Mock<ISiteService>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("A1876", "Medical")]
    public async Task OnPostAsync_IfInvalidSearchParameters_DisplaysSearchInvalid(string providerOdsCode, string providerName)
    {
      var searchModel = new SearchModel(_mockOptions.Object, _mockSiteService.Object);

      searchModel.ProviderOdsCode = providerOdsCode;
      searchModel.ProviderName = providerName;

      var result = await searchModel.OnPostSearchAsync();

      Assert.True(searchModel.DisplaySearchInvalid);
    }

    [Theory]
    [InlineData("$$$", "")]
    [InlineData("A_163763", "")]
    public async Task OnPostAsync_IfInvalidModel_ReturnValidationError(string providerOdsCode, string providerName)
    {
      var searchModel = new SearchModel(_mockOptions.Object, _mockSiteService.Object);

      searchModel.ProviderOdsCode = providerOdsCode;
      searchModel.ProviderName = providerName;

      searchModel.ModelState.AddModelError("ProviderOdsCode", "You must enter a valid value for Provider ODS Code");

      var result = await searchModel.OnPostSearchAsync();

      Assert.IsType<PageResult>(result);
    }

    [Theory]
    [InlineData("A20047", "")]
    [InlineData("A20047, B82617", "")]
    [InlineData("", "Medical")]
    [InlineData("", "The, Practice")]
    public async Task OnPostAsync_IfValidSearchParameters_ReturnSearchResult(string providerOdsCode, string providerName)
    {
      var searchModel = new SearchModel(_mockOptions.Object, _mockSiteService.Object);

      searchModel.ProviderOdsCode = providerOdsCode;
      searchModel.ProviderName = providerName;

      var result = await searchModel.OnPostSearchAsync();

      Assert.IsType<PageResult>(result);
      Assert.True(searchModel.SearchResult != null);      
    }
  }
}
