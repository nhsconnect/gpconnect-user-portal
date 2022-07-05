using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

public class Organization
{
  
    public class OrganizationAddress
    {
        [JsonProperty("line")]
        public List<string> Lines { get; set; }
        public string City { get; set; }
        [JsonProperty("district")]
        public string County { get; set; }
        [JsonProperty("postalcode")]
        public string Postcode { get; set; }
        public string Country { get; set; }
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public OrganizationAddress Address { get; set; }
}

// {
//     "id": "FKG31",
//     "name": "QUEENS PHARMACY",
//     "address": {
//         "line": [
//             "12 QUEENSTOWN ROAD",
//             "BATTERSEA"
//         ],
//         "city": "LONDON",
//         "district": "GREATER LONDON",
//         "postalCode": "SW8 3RX",
//         "country": "ENGLAND"
//     }
// }