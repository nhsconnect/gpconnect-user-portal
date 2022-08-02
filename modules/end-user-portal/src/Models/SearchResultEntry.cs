using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchResultEntry
{
    [JsonProperty("id")]
    public string SiteDefinitionId { get; set; } = "";

    [JsonProperty("odsCode")]
    [Display(Name = "SiteOdsCode", ResourceType = typeof(DataFieldNameResources))]
    public string SiteODSCode { get; set; } = "";

    [JsonProperty("name")]
    [Display(Name = "SiteName", ResourceType = typeof(DataFieldNameResources))]
    public string SiteName { get; set; } = "";

    [JsonProperty("addressLine1")]
    public string SiteAddressLine1 { get; set; } = "";

    [JsonProperty("addressLine2")]
    public string SiteAddressLine2 { get; set; } = "";

    [JsonProperty("town")]
    public string SiteAddressTown { get; set; } = "";

    [JsonProperty("county")]
    public string SiteAddressCounty { get; set; } = "";

    [JsonProperty("country")]
    public string SiteAddressCountry { get; set; } = "";

    [JsonProperty("postcode")]
    public string SitePostcode { get; set; } = "";


    public string FullAddress => GetFullAddressAsString();

    private string GetFullAddressAsString()
    {
        var addressLines = new List<string> {
                SiteAddressLine1,
                SiteAddressLine2,
                SiteAddressTown,
                SiteAddressCounty,
                SitePostcode,
                SiteAddressCountry
            };

        return string.Join(", ", addressLines.Where(s => !string.IsNullOrEmpty(s)));
    }

    [JsonProperty("useCase")]
    [Display(Name = "UseCaseDescription", ResourceType = typeof(DataFieldNameResources))]
    public string UseCaseDescription { get; set; } = "";
}
