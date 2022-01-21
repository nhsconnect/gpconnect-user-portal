using Newtonsoft.Json;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class OrganisationDetail
    {
        [JsonProperty("Organisation")]
        public Organisation Organisation { get; set; }
        public string PostCode => Organisation?.GeoLoc?.Location.PostCode;
        public string SiteName => Organisation?.Name;
        public string TelephoneNumber => Organisation?.Contacts?.Contact?.FirstOrDefault(x => x.type == "tel")?.value;
        public string CCGOdsCode => Organisation?.Rels?.Rel?.FirstOrDefault(x => x.Target?.PrimaryRoleId?.id == "RO98")?.Target?.OrgId?.Extension;
    }
}
