using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class PrimaryRoleId
    {
        [JsonProperty("id")]
        public string id { get; set; }
    }
}