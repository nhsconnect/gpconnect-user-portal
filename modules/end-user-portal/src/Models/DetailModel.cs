using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class DetailModel : BaseModel
{
    public SearchResultEntry SearchResultEntry { get; set; } = null;

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public SearchMode Mode { get; set; } = SearchMode.Name;
    
    [BindProperty(SupportsGet = true)]
    public DetailViewSource Source { get; set; } = DetailViewSource.Search;
}
