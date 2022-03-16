using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Identifier
    {
        [JsonProperty("system")]
        public string System { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}