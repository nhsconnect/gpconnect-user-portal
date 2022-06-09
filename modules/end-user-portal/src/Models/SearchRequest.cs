using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchRequest
{
  [JsonProperty("provider_code")]
  public string? SiteOdsCode { get; set; }

  [JsonProperty("provider_name")]
  public string? SiteName { get; set; }  
}
