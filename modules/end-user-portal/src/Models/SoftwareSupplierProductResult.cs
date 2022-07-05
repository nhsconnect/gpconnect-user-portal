using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SoftwareSupplierProductResult
{
    [JsonProperty("supplierProductId")]
    public int SoftwareSupplierProductId { get; set; } = 0;

    [JsonProperty("supplierProductValue")]
    public string SoftwareSupplierProduct { get; set; } = "";
}
