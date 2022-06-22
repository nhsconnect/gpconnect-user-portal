using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Search;
public class BackPartialModelTest
{
    public BackPartialModelTest()
    {

    }

    [Theory]
    [InlineData(DetailViewSource.Results, null, "/Search/Results")]
    [InlineData(DetailViewSource.Search, SearchMode.Code, "/Search/Code")]
    [InlineData(DetailViewSource.Search, SearchMode.Name, "/Search/Name")]
    public void PageName_GivenSourceParameter_ReturnsExpectedPage(DetailViewSource source, SearchMode mode, string expectedPageName)
    {
        var model = new BackPartialModel
        {
            Source = source,
            Mode = mode
        };

        var result = model.PageName;

        Assert.Equal(result, expectedPageName);
    }

    [Theory]
    [InlineData(SearchMode.Code, "providerOdsCode")]
    [InlineData(SearchMode.Name, "providerName")]
    public void Params_GivenSearchAsSource_ReturnsExpectedQueryParams(SearchMode mode, string expectedParamName)
    {
        var expectedQuery = "someQueryText";

        var model = new BackPartialModel
        {
            Source = DetailViewSource.Search,
            Mode = mode,
            Query = expectedQuery
        };

        var result = model.Params;

        Assert.Equal(expectedQuery, result[expectedParamName]);
    }

    [Fact]
    public void Params_GivenResultsToDetailedView_ReturnsExpectedQueryParams()
    {
        var expectedQuery = "someQueryText";

        var model = new BackPartialModel
        {
            Source = DetailViewSource.Results,
            Mode = SearchMode.Code,
            Query = expectedQuery,
            ResultsPageNumber = 100
        };

        var result = model.Params;

        Assert.Equal(expectedQuery, result["Query"]);
        Assert.Equal(SearchMode.Code.ToString(), result["Mode"]);
        Assert.Equal("100", result["PageNumber"]);
    }
}