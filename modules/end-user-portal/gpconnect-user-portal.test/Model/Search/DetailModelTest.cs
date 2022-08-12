using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;
public class DetailModelTest
{
    private readonly Mock<ISiteService> _mockSiteService;

    public DetailModelTest()
    {
        _mockSiteService = new Mock<ISiteService>();
        _mockSiteService.Setup(mss => mss.SearchSiteAsync(It.IsAny<string>())).ReturnsAsync(new SearchResultEntry());
    }

    [Fact]
    public void OnGet_SearchSite_WhenQueryParameterIsNull_Throws()
    {
        _mockSiteService.Setup(mss => mss.SearchSiteAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Boom!!!"));

        var detailModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = null,
            Mode = SearchMode.Name,
            Source = DetailViewSource.Search
        };

        Assert.Null(detailModel.SearchResultEntry);
        Assert.ThrowsAsync<Exception>(async () => await detailModel.OnGet("id"));
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

    [Fact]
    public async Task OnGet_SearchSite_WhenSiteServiceReturnsNull_ReturnsNotFound()
    {
        _mockSiteService.Setup(mss => mss.SearchSiteAsync(It.IsAny<string>())).ReturnsAsync((SearchResultEntry)null);

        var detailModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "query",
            Mode = SearchMode.Name,
            Source = DetailViewSource.Search
        };

        var result = await detailModel.OnGet("id");

        Assert.Null(detailModel.SearchResultEntry);
        Assert.IsType<NotFoundResult>(result);
    }

    [Theory]
    [InlineData("query", SearchMode.Code)]
    [InlineData("some-query", SearchMode.Name)]
    public void BackPartial_GivenSourceIsSearch_ReadsExpectedParameters_ReturnsModel(string query, SearchMode mode)
    {
        var resultsModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Source = DetailViewSource.Search,
            Query = query,
            Mode = mode
        };

        var result = resultsModel.BackPartial; 

        Assert.Equal(query, result.Query);
        Assert.StrictEqual(mode, result.Mode);
        Assert.StrictEqual(DetailViewSource.Search, result.Source);
    }

    [Theory]
    [InlineData("query", SearchMode.Code, 1)]
    [InlineData("some-query", SearchMode.Name, 5)]
    public void BackPartial_GivenSourceIsResults_ReadsExpectedParameters_ReturnsModel(string query, SearchMode mode, int pageNumber)
    {
        var resultsModel = new DetailModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Source = DetailViewSource.Results,
            Query = query,
            Mode = mode,
            ResultsPageNumber = pageNumber
        };

        var result = resultsModel.BackPartial; 

        Assert.Equal(query, result.Query);
        Assert.StrictEqual(mode, result.Mode);
        Assert.StrictEqual(DetailViewSource.Results, result.Source);
        Assert.StrictEqual(pageNumber, result.ResultsPageNumber);
    }

}
