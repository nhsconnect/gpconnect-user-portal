using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Entry
    {
        [JsonProperty("resource")] 
        public Resource Resource { get; set; }
    }
}
