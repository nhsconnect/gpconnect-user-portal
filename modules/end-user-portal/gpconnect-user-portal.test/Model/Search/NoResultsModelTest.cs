using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;
public class NoResultsModelTest
{
    [Fact]
    public void NameQueryOrNull_WhenModeIsName_ReturnsQueryText()
    {
        var resultsModel = new NoResultsModel(Mock.Of<IOptions<ApplicationParameters>>())
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Equal("Query", resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void NameQueryOrNull_WhenModeIsCode_ReturnsNull()
    {
        var resultsModel = new NoResultsModel(Mock.Of<IOptions<ApplicationParameters>>())
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Null(resultsModel.NameQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsCode_ReturnsQueryText()
    {
        var resultsModel = new NoResultsModel(Mock.Of<IOptions<ApplicationParameters>>())
        {
            Query = "Query",
            Mode = SearchMode.Code
        };

        Assert.Equal("Query", resultsModel.CodeQueryOrNull);
    }

    [Fact]
    public void CodeQueryOrNull_WhenModeIsName_ReturnsNull()
    {
        var resultsModel = new NoResultsModel(Mock.Of<IOptions<ApplicationParameters>>())
        {
            Query = "Query",
            Mode = SearchMode.Name
        };

        Assert.Null(resultsModel.CodeQueryOrNull);
    }

    [Theory]
    [InlineData(SearchMode.Code, "Organisation ODS Code")]
    [InlineData(SearchMode.Name, "Organisation Name")]
    public void SearchType_ReturnsExpectedText(SearchMode mode, string text)
    {
        var resultsModel = new NoResultsModel(Mock.Of<IOptions<ApplicationParameters>>())
        {
            Mode = mode
        };

        Assert.Equal(text, resultsModel.SearchType);
    }
}
