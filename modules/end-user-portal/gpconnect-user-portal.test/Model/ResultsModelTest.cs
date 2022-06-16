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
public class ResultsModelTest
{
    private readonly Mock<ISiteService> _mockSiteService;

    public ResultsModelTest()
    {
        _mockSiteService = new Mock<ISiteService>();

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(new List<SearchResultEntry>());
    }

    [Fact]
    public void NameQueryOrNull_WhenModeIsName_ReturnsQueryText()
    {
        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Equal("Query", resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void NameQueryOrNull_WhenModeIsCode_ReturnsNull()
    {
        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Null(resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsCode_ReturnsQueryText()
    {
        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Equal("Query", resultsModel.CodeQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsName_ReturnsNull()
    {
        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Null(resultsModel.CodeQueryOrNull);
    }

    [Theory]
    [InlineData(SearchMode.Name)]
    [InlineData(SearchMode.Code)]
    public async Task OnGet_CallsSearchSites_WithExpectedParameters(SearchMode mode)
    {
        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = mode
        };

        await resultsModel.OnGet();

        _mockSiteService.Verify(mss => mss.SearchSitesAsync("Query", mode), Times.Once);
    }

    [Fact]
    public void OnGet_SearchSitesThrows_Throws()
    {
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ThrowsAsync(new Exception("Boom!!!"));

        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.ThrowsAsync<Exception>(async () => await resultsModel.OnGet());
    }

    [Theory]
    [InlineData(SearchMode.Name)]
    [InlineData(SearchMode.Code)]
    public async Task OnGet_SearchSites_ReturnsNoResult_RedirectsToNoResultsPage(SearchMode mode)
    {
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(new List<SearchResultEntry>());

        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = mode
        };

        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./NoResults",result?.PageName);
        Assert.StrictEqual(2, result?.RouteValues?.Count);

        Assert.Equal("Query",result?.RouteValues?.GetValueOrDefault("query"));
        Assert.StrictEqual(mode, result?.RouteValues?.GetValueOrDefault("mode"));

    }

    [Fact]
    public async Task OnGet_SearchSites_ReturnsOneResult_RedirectsToDetailsPage()
    {
        Guid expected = Guid.NewGuid();

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(new List<SearchResultEntry>{
            new SearchResultEntry {
                SiteDefinitionId = expected
            }
        });

        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./Detail",result?.PageName);
        Assert.StrictEqual(1, result?.RouteValues?.Count);

        Assert.StrictEqual(expected, result?.RouteValues?.GetValueOrDefault("id"));
    }

    [Fact]
    public async Task OnGet_SearchSites_ReturnsMoreThanOneResult_SetsTheSearchResultsOnTheModel_InAlphabeticalOrder()
    {
        var unorderedList = new List<SearchResultEntry>
        {
            new SearchResultEntry
            {
                SiteName = "gp"
            },
            new SearchResultEntry
            {
                SiteName = "connect"
            }
        };
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(unorderedList);

        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        await resultsModel.OnGet();

        Assert.Equal("connect", resultsModel.SearchResult.SearchResults[0].SiteName);
        Assert.Equal("gp", resultsModel.SearchResult.SearchResults[1].SiteName);
    }

    [Fact]
    public async Task OnGet_SearchSites_ReturnsMoreThanOneResult_RendersPage()
    {
        var results = new List<SearchResultEntry>
        {
            new SearchResultEntry
            {
                SiteName = "gp"
            },
            new SearchResultEntry
            {
                SiteName = "connect"
            }
        };

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>())).ReturnsAsync(results);

        var resultsModel = new ResultsModel(Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };
        
        var result = await resultsModel.OnGet();
        
        Assert.IsType<PageResult>(result);
    }
}
