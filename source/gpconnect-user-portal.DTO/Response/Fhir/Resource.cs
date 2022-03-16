using Newtonsoft.Json;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Resource
    {
        [JsonProperty("id")] 
        public string SiteIdentifier { get; set; }
        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }
        [JsonProperty("extension")] 
        public List<Extension> Extension { get; set; }        
        [JsonProperty("owner")]
        public Owner Owner { get; set; }
    }
}