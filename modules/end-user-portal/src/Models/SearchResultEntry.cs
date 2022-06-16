using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchResultEntry
{
  [JsonProperty("id")]
  public Guid SiteDefinitionId { get; set; }

  [JsonProperty("odsCode")]
  [Display(Name = "SiteOdsCode", ResourceType = typeof(DataFieldNameResources))]
  public string SiteODSCode { get; set; } = "";

  [JsonProperty("name")]
  [Display(Name = "SiteName", ResourceType = typeof(DataFieldNameResources))]
  public string SiteName { get; set; } = "";

  [JsonProperty("postcode")]
  [Display(Name = "SitePostcode", ResourceType = typeof(DataFieldNameResources))]
  public string SitePostcode { get; set; } = "";

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

  [JsonProperty("address1")]
  public string Address1 { get; set; } = "";

  [JsonProperty("address2")]
  public string Address2 { get; set; } = "";

  [JsonProperty("town")]
  public string Town { get; set; } = "";

  [JsonProperty("county")]
  public string County { get; set; } = "";

  [JsonProperty("country")]
  public string Country { get; set; } = "";

  [JsonIgnore]
  public List<string> AddressFields => new List<string> {
        SiteName,
        Address1,
        Address2,
        Town,
        County,
        SitePostcode
    }.FindAll((x) => !string.IsNullOrWhiteSpace(x));
}
