using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test
{
  public class SearchPageTests
  {
    private readonly Mock<ILogger<SearchModel>> _mockLogger;
    private readonly Mock<IOptions<Core.ApplicationParameters>> _mockOptions;
    private readonly Mock<IRequestService> _mockRequestService;


    public SearchPageTests()
    {
      _mockLogger = new Mock<ILogger<SearchModel>>();
      _mockOptions = new Mock<IOptions<Core.ApplicationParameters>>();
      _mockRequestService = new Mock<IRequestService>();

    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("A1876", "Medical")]
    public async Task OnPost_IfInvalidSearchParameters_DisplaysSearchInvalid(string providerOdsCode, string providerName)
    {
      var searchModel = new SearchModel(_mockLogger.Object, _mockOptions.Object, _mockRequestService.Object);

      searchModel.ProviderOdsCode = providerOdsCode;
      searchModel.ProviderName = providerName;

      var result = await searchModel.OnPostSearchAsync();
      Assert.True(searchModel.DisplaySearchInvalid);
    }

    [Theory]
    [InlineData("$$$", "")]
    [InlineData("A_163763", "")]
    public async Task OnPost_IfInvalidModel_ReturnValidationError(string providerOdsCode, string providerName)
    {
      var searchModel = new SearchModel(_mockLogger.Object, _mockOptions.Object, _mockRequestService.Object);

      searchModel.ProviderOdsCode = providerOdsCode;
      searchModel.ProviderName = providerName;

      searchModel.ModelState.AddModelError("ProviderOdsCode", "You must enter a valid value for Provider ODS Code");

      var result = await searchModel.OnPostSearchAsync();
      Assert.IsType<PageResult>(result);
    }
  }
}
