using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class GpConnectInteractionForSupplier
{
    [JsonProperty("gpConnectInteractionForSupplierId")]
    public int GpConnectInteractionForSupplierId { get; set; } = 0;

    [JsonProperty("gpConnectInteractionForSupplierValue")]
    public string GpConnectInteractionForSupplierValue { get; set; } = "";

    [Display(Name = "GpConnectInteractionForSupplier", ResourceType = typeof(DataFieldNameResources))]
    public bool Selected { get; set; }
}
