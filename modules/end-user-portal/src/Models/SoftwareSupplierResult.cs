using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SoftwareSupplierResult
{
    [JsonProperty("supplierId")]
    public int SoftwareSupplierId { get; set; } = 0;

    [JsonProperty("supplierValue")]
    public string SoftwareSupplierName { get; set; } = "";  
}
