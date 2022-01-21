using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class ValueReference
    {
        [JsonProperty("identifier")] 
        public Identifier Identifier { get; set; }
    }
}