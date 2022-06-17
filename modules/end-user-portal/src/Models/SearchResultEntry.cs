using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchResultEntry
{
    [JsonProperty("id")]
    public string SiteDefinitionId { get; set; }

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

    public string FullAddress => AddressBuilder.GetFullAddress(new List<string>() { SiteAddressLine1, SiteAddressLine2 }, SiteAddressTown, SiteAddressCounty, SitePostcode, SiteAddressCountry);

    [JsonIgnore]
    
    [JsonProperty("ccgIcbOdsCode")]
    [Display(Name = "CcgIcbOdsCode", ResourceType = typeof(DataFieldNameResources))]
    public string CcgIcbOdsCode { get; set; } = "";

    [JsonProperty("ccgIcbName")]
    [Display(Name = "CcgIcbName", ResourceType = typeof(DataFieldNameResources))]
    public string CcgIcbName { get; set; } = "";

    [JsonProperty("useCase")]
    [Display(Name = "UseCaseDescription", ResourceType = typeof(DataFieldNameResources))]
    public string UseCaseDescription { get; set; } = "";

    [JsonProperty("accessRecordHTMLEnabled")]
    [Display(Name = "HasHtmlView", ResourceType = typeof(DataFieldNameResources))]
    public bool HasHtmlView { get; set; }

    [JsonProperty("structuredRecordEnabled")]
    [Display(Name = "HasStructured", ResourceType = typeof(DataFieldNameResources))]
    public bool HasStructured { get; set; }

    [JsonProperty("appointmentManagementEnabled")]
    [Display(Name = "HasAppointmentManagement", ResourceType = typeof(DataFieldNameResources))]
    public bool HasAppointmentManagement { get; set; }

    [JsonProperty("sendDocumentEnabled")]
    [Display(Name = "HasSendDocument", ResourceType = typeof(DataFieldNameResources))]
    public bool HasSendDocument { get; set; }

    public List<string> AddressFields => new List<string> {
            SiteName,
            SiteAddressLine1,
            SiteAddressLine2,
            SiteAddressTown,
            SiteAddressCounty,
            SitePostcode
        }.FindAll((x) => !string.IsNullOrWhiteSpace(x));
}
