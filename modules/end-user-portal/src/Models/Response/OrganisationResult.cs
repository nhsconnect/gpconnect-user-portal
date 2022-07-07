using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

public class OrganisationResult
{
    [JsonProperty("id")]
    public string OdsCode { get; set; } = "";

    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("address")]
    public OrganisationAddress Address { get; set; }
}

public class OrganisationAddress
{
    [JsonProperty("line")]
    public List<string> AddressLines { get; set; }

    [JsonProperty("city")]
    public string City { get; set; } = "";

    [JsonProperty("district")]
    public string County { get; set; } = "";

    [JsonProperty("postalCode")]
    public string Postcode { get; set; } = "";

    [JsonProperty("country")]
    public string Country { get; set; } = "";

    public string FullAddress => GetFullAddressAsString();

    private string GetFullAddressAsString()
    {
        var addressLines = new List<string> {
            string.Join(", ", AddressLines),
            City,
            County,
            Postcode,
            Country
        };

        return string.Join(", ", addressLines.Where(s => !string.IsNullOrEmpty(s)));
    }
}
