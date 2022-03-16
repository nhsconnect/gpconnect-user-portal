using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class OrgId
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}