using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
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
    private readonly Mock<IOptions<ResultPageConfig>> _mockConfig;
    
    private readonly ResultPageConfig _config;

    public ResultsModelTest()
    {
        _config = new ResultPageConfig();

        _mockSiteService = new Mock<ISiteService>();
        _mockConfig = new Mock<IOptions<ResultPageConfig>>();

        _mockConfig.SetupGet(mc => mc.Value).Returns(_config);
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new SearchResult {
            SearchResults = new List<SearchResultEntry>()
        });
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
        var expectedIndex = 201; // ((3 - 1) * 100) + 1

        _config.ResultsPerPage = resultsPerPage;
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = mode,
            PageNumber = requestedPageNumber
        };

        await resultsModel.OnGet();

        _mockSiteService.Verify(mss => mss.SearchSitesAsync("Query", mode, expectedIndex, resultsPerPage), Times.Once);
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
        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new SearchResult());

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
    public async Task OnGet_SearchSites_ReturnsTotalOneResult_RedirectsToDetailsPage()
    {
        Guid expected = Guid.NewGuid();
        const string expectedQuery = "Query";
        const SearchMode expectedSearchMode = SearchMode.Name;
        const int expectedPage = 1;

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new SearchResult {
            TotalResults = 1,
            SearchResults = new List<SearchResultEntry>{
                new SearchResultEntry {
                    SiteDefinitionId = expected.ToString()
                }
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

        var response = new SearchResult
        {
            TotalResults = 300,
            SearchResults = new List<SearchResultEntry>()
        };

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(response);
        
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

    [Theory]
    [InlineData(31,30)]
    [InlineData(31,1)]
    public async Task OnGet_SearchSites_ReturnsPagedResults_RendersPage(int totalResults, int currentPageResults)
    {
        var response = new SearchResult
        {
            TotalResults = totalResults,
            SearchResults = BuildResults(currentPageResults)
        };

        _mockSiteService.Setup(mss => mss.SearchSitesAsync(It.IsAny<string>(), It.IsAny<SearchMode>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(response);

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = "Query",
            Mode = SearchMode.Name
        };
        
        var result = await resultsModel.OnGet();
        
        Assert.IsType<PageResult>(result);
    }

    [Theory]
    [InlineData("query", SearchMode.Code)]
    [InlineData("some-query", SearchMode.Name)]
    public void BackPartial_ReadsExpectedParameters_ReturnsModel(string query, SearchMode mode)
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            Query = query,
            Mode = mode
        };

        var result = resultsModel.BackPartial; 

        Assert.Equal(query, result.Query);
        Assert.StrictEqual(mode, result.Mode);
        Assert.StrictEqual(DetailViewSource.Search, result.Source);
    }

    [Theory]
    [InlineData(1, 10, 1)]
    [InlineData(31, 10, 4)]
    [InlineData(30, 10, 3)]
    public void NumPages_CalculatesNumPagesForTotalResults(int results, int resultsPerPage, int expectedPages)
    {
        _config.ResultsPerPage = resultsPerPage;

        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            SearchResult = new SearchResult
            {
                TotalResults = results
            }
        };

        var result = resultsModel.NumPages; 

        Assert.StrictEqual(expectedPages, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(27)]
    public void CurrentPageNumber_ReadsFromModel_ReturnsExpected(int pageNumber)
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            PageNumber = pageNumber
        };

        var result = resultsModel.CurrentPageNumber; 

        Assert.StrictEqual(pageNumber, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(27)]
    public void TotalResults_ReadsFromSearchResult_ReturnsExpected(int numResults)
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            SearchResult = new SearchResult
            {
                TotalResults = numResults
            }
        };

        var result = resultsModel.TotalResults; 

        Assert.StrictEqual(numResults, result);
    }

    [Theory]
    [InlineData(1, 1, false)]
    [InlineData(2, 3, true)]
    [InlineData(27, 30, true)]
    [InlineData(30, 30, false)]
    public void HasMoreResults_ReturnsExpected_ForGivenPageNumber(int pageNumber, int numPages, bool expectedResult)
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            PageNumber = pageNumber,
            SearchResult = new SearchResult
            {
                TotalResults = numPages * _config.ResultsPerPage
            }
        };

        var result = resultsModel.HasMoreResults; 

        Assert.StrictEqual(expectedResult, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(27, true)]
    public void HasPreviousResults_ReturnsExpected_ForGivenPageNumber(int pageNumber, bool expectedResult)
    {
        var resultsModel = new ResultsModel(_mockConfig.Object, Mock.Of<IOptions<ApplicationParameters>>(), _mockSiteService.Object)
        {
            PageNumber = pageNumber
        };

        var result = resultsModel.HasPreviousResults; 

        Assert.StrictEqual(expectedResult, result);
    }

    private List<SearchResultEntry> BuildResults(int currentPageResults)
    {
        var results = new List<SearchResultEntry>();
        for (int i = 0; i < currentPageResults; i++)
        {
            results.Add(new SearchResultEntry {
                SiteName = $"SiteName{i}"
            });
        }
        return results;
    }
}
