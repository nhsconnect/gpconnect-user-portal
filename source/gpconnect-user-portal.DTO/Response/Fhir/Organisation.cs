using Newtonsoft.Json;
using System;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Organisation
    {
        [JsonProperty("Name")] 
        public string Name { get; set; }
        [JsonProperty("GeoLoc")]
        public GeoLoc GeoLoc { get; set; }
        [JsonProperty("Contacts")]
        public Contacts Contacts { get; set; }
        [JsonProperty("Rels")]
        public Rels Rels { get; set; }
    }
}
