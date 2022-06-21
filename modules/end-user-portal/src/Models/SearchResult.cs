using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchResult
{
    [JsonProperty("results")]
    public List<SearchResultEntry> SearchResults { get; set; } = new List<SearchResultEntry>();
    
    [JsonProperty("totalResults")]
    public int TotalResults { get; set; } = 0;
}
