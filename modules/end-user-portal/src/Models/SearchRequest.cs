using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchRequest
{
    public string Query { get; set; } = "";
    public SearchMode Mode { get; set; }
}
