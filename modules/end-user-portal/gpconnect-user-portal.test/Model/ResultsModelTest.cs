using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Config;
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
    private readonly Mock<IOptions<ResultPageConfig>> _mockConfig;
    
    private readonly ResultPageConfig _config;

    public ResultsModelTest()
    {
        _config = new ResultPageConfig();

        _mockSiteService = new Mock<ISiteService>();
        _mockConfig = new Mock<IOptions<ResultPageConfig>>();

        _mockConfig.SetupGet(mc => mc.Value).Returns(_config);
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SearchResultEntry>());
    }

    [Fact]
    public void NameQueryOrNull_WhenModeIsName_ReturnsQueryText()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Equal("Query", resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void NameQueryOrNull_WhenModeIsCode_ReturnsNull()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Null(resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsCode_ReturnsQueryText()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Equal("Query", resultsModel.CodeQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsName_ReturnsNull()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Null(resultsModel.CodeQueryOrNull);
    }

    [Fact]
    public async Task OnGet_ModelStateInvalid_ReturnsRedirectToSearchPage()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object);
        resultsModel.ModelState.AddModelError(String.Empty, "Adding Error to invalidate the model state");

        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./Name", result?.PageName);
    }

    [Fact]
    public async Task OnGet_PageNumberLessThan_ReturnsRedirectToResultPage()
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            PageNumber = 0,
            Query = "Query",
        };
        
        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./Results", result?.PageName);
        Assert.StrictEqual(2, result?.RouteValues?.Count);
        Assert.StrictEqual(2, result?.RouteValues?.Count);

        Assert.Equal("Query",result?.RouteValues?.GetValueOrDefault("query"));
        Assert.StrictEqual(SearchMode.Name, result?.RouteValues?.GetValueOrDefault("mode"));
    }

    [Theory]
    [InlineData(SearchMode.Name)]
    [InlineData(SearchMode.Code)]
    public async Task OnGet_CallsSearchSites_WithExpectedParameters(SearchMode mode)
    {
        var requestedPageNumber = 3;
        var resultsPerPage = 100;

        _config.ResultsPerPage = resultsPerPage;
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = mode,
            PageNumber = requestedPageNumber
        };

        await resultsModel.OnGet();

        _mockSiteService.Verify(mss => mss.SearchSitesAsync("Query", mode, (requestedPageNumber - 1) * resultsPerPage, resultsPerPage), Times.Once);
    }

    [Fact]
    public void OnGet_SearchSitesThrows_Throws()
    {
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Boom!!!"));

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
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
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SearchResultEntry>());

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
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
        const string expectedQuery = "Query";
        const SearchMode expectedSearchMode = SearchMode.Name;
        const int expectedPage = 1;

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SearchResultEntry>{
            new SearchResultEntry {
                SiteDefinitionId = expected.ToString()
            }
        });
        
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = expectedQuery,
            Mode = expectedSearchMode,
            PageNumber = expectedPage
        };

        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./Detail",result?.PageName);
        Assert.StrictEqual(4, result?.RouteValues?.Count);

        Assert.Equal(expected.ToString(), result?.RouteValues?.GetValueOrDefault("id"));
        Assert.Equal(expectedSearchMode, result?.RouteValues?.GetValueOrDefault("mode"));
        Assert.Equal(expectedQuery, result?.RouteValues?.GetValueOrDefault("query"));
        Assert.Equal(expectedPage, result?.RouteValues?.GetValueOrDefault("page"));
    }

    [Fact]
    public async Task OnGet_SearchSites_ReturnsLessPagesThanPageNumder_RedirectsToResultsPage()
    {
        Guid expected = Guid.NewGuid();
        const string expectedQuery = "Query";
        const SearchMode expectedSearchMode = SearchMode.Name;
        const int expectedPage = 10;

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SearchResultEntry>());
        
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = expectedQuery,
            Mode = expectedSearchMode,
            PageNumber = expectedPage + 1
        };

        var result = await resultsModel.OnGet() as RedirectToPageResult;

        Assert.Equal("./Results",result?.PageName);
        Assert.StrictEqual(3, result?.RouteValues?.Count);

        Assert.Equal(expectedSearchMode, result?.RouteValues?.GetValueOrDefault("mode"));
        Assert.Equal(expectedQuery, result?.RouteValues?.GetValueOrDefault("query"));
        Assert.Equal(expectedPage, result?.RouteValues?.GetValueOrDefault("pageNumber"));
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
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(unorderedList);

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
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

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(results);

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };
        
        var result = await resultsModel.OnGet();
        
        Assert.IsType<PageResult>(result);
    }
}
