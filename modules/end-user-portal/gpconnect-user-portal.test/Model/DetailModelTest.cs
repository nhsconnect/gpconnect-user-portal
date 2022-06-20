using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;
public class DetailModelTest
{
    private readonly Mock<ISiteService> _mockSiteService;

    public DetailModelTest()
    {
        _mockSiteService = new Mock<ISiteService>();

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(new List<SearchResultEntry>());
    }

    [Fact]
    public async Task OnGet_CallsSearchSite_WithExpectedParameters()
    {
        var expectedId = "expected";

        var detailModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object);

        await detailModel.OnGet(expectedId);

        _mockSiteService.Verify(mss => mss.SearchSiteAsync(expectedId), Times.Once);
    }

    [Fact]
    public void OnGet_SearchSiteThrows_Throws()
    {
        _mockSiteService.Setup(mss => mss.SearchSiteAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Boom!!!"));

        var detailModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object);

        Assert.ThrowsAsync<Exception>(async () => await detailModel.OnGet("id"));
    }
}
