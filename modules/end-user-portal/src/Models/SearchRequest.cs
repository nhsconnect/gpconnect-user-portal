using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchRequest
{
  [JsonProperty("provider_code")]
  public string? SiteOdsCode { get; set; }

  [JsonProperty("provider_name")]
  public string? SiteName { get; set; }

  [JsonProperty("ccg_code")]
  public string? CcgIcbOdsCode { get; set; }

  [JsonProperty("ccg_name")]
  public string? CcgIcbName { get; set; }
}
