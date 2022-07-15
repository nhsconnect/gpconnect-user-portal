using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class GpConnectInteractionForSupplier
{
    [JsonProperty("id")]
    public int Id { get; set; } = 0;

    [JsonProperty("value")]
    public string Value { get; set; } = "";

    [Display(Name = "GpConnectInteractionForSupplier", ResourceType = typeof(DataFieldNameResources))]
    public bool Selected { get; set; }
}
