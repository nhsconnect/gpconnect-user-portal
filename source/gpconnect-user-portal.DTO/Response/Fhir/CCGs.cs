using Newtonsoft.Json;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class CCGs
    {
        [JsonProperty("Organisations")] 
        public List<CCGDetail> CCG { get; set; }
    }
}
