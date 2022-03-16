using Newtonsoft.Json;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Target
    {
        [JsonProperty("OrgId")]
        public OrgId OrgId { get; set; }
        [JsonProperty("PrimaryRoleId")]
        public PrimaryRoleId PrimaryRoleId { get; set; }
    }
}