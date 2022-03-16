using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Extension
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("valueReference")]
        public ValueReference ValueReference { get; set; }
    }
}