using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class CCGDetail
    {
        [JsonProperty("OrgId")]
        public string OdsCode { get; set; }
        [JsonProperty("Name")]
        public string OrganisationName { get; set; }
    }
}