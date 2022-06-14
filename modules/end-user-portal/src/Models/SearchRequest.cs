using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchRequest
{
    [JsonProperty("query")]
    public string Query { get; set; } = "";
    [JsonProperty("mode")]
    public SearchModeEnums Mode { get; set; }
}
