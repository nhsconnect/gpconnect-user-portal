namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

public class Organization
{
  
    public class OrganizationAddress
    {
      public List<string> Lines { get; set; }
      public string City { get; set; }
      public string District { get; set; }
      public string PostalCode { get; set; }
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