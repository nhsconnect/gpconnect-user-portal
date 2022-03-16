using Newtonsoft.Json;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class Contacts
    {
        [JsonProperty("Contact")]
        public List<Contact> Contact { get; set; }
    }

    public class Contact
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
    }
}