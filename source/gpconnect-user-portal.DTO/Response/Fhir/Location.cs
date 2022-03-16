using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Location
    {
        [JsonProperty("AddrLn1")] 
        public string AddrLn1 { get; set; }
        [JsonProperty("AddrLn2")]
        public string AddrLn2 { get; set; }
        [JsonProperty("AddrLn3")]
        public string AddrLn3 { get; set; }
        [JsonProperty("Town")]
        public string Town { get; set; }
        [JsonProperty("County")]
        public string County { get; set; }
        [JsonProperty("PostCode")]
        public string PostCode { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
    }
}