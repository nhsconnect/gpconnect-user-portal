using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SoftwareSupplierProductResult
{
    [JsonProperty("supplierProductId")]
    public int SoftwareSupplierProductId { get; set; } = 0;

    [JsonProperty("supplierProductValue")]
    public string SoftwareSupplierProduct { get; set; } = "";

    [Display(Name = "SoftwareSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
    public bool Selected { get; set; }
}
