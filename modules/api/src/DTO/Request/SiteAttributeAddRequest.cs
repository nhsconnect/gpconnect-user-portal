using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class SiteAttributeAddRequest
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    [JsonProperty("value")]
    public string Value { get; set; } = "";
}
