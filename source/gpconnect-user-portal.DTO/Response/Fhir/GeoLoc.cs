using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class GeoLoc
    {
        [JsonProperty("Location")]
        public Location Location { get; set; }
    }
}