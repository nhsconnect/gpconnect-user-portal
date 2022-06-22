using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public class BackPartialModel
{
    public string Query { get; set; }
    public SearchMode Mode { get; set; } = SearchMode.Name;
    public DetailViewSource Source { get; set; } = DetailViewSource.Search;
    public int ResultsPageNumber { get; set; } = 1;

    public string PageName => Source == DetailViewSource.Results ? "/Search/Results" : $"/Search/{Mode}";
    public IDictionary<string, string> Params => GenerateRouteParamsDictionary();
    private IDictionary<string, string> GenerateRouteParamsDictionary()
    {
        if (Source == DetailViewSource.Search)
        {
            return new Dictionary<string,string> 
            {
                { ParamNameForMode(Mode), Query }
            };
        }

        return new Dictionary<string,string>
        {
            { "Query", Query },
            { "Mode", Mode.ToString() },
            { "PageNumber", ResultsPageNumber.ToString() }
        };
    }

    private string ParamNameForMode(SearchMode mode)
    {
        return mode == SearchMode.Code ? "providerOdsCode" : "providerName";
    }
}
