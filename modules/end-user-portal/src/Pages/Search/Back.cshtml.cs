using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public class BackPartialModel
{
    public string Query { get; set; }
    public SearchMode Mode { get; set; } = SearchMode.Name;
    public DetailViewSource Source { get; set; } = DetailViewSource.Search;
    public int ResultsPageNumber { get; set; } = 1;

    public string PageName => Source == DetailViewSource.Results ? "/Search/Results" : $"/Search/{Mode}";
}
