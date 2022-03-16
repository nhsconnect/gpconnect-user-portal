using Newtonsoft.Json;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Rels
    {
        [JsonProperty("Rel")]
        public List<Rel> Rel { get; set; }
    }

    public class Rel
    {
        [JsonProperty("Status")] 
        public string Status { get; set; }
        [JsonProperty("Target")] 
        public Target Target { get; set; }
    }
}