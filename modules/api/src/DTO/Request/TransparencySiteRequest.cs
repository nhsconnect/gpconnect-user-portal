using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class TransparencySiteRequest
{
    [BindProperty(Name = "provider_code", SupportsGet = true)]
    public string? ProviderCode { get; set; }

    [BindProperty(Name = "provider_name", SupportsGet = true)]
    public string? ProviderName { get; set; }

    [BindProperty(Name = "start", SupportsGet = true)]
    public int? StartPosition { get; set; } = 1;

    [BindProperty(Name = "count", SupportsGet = true)]
    public int? Count { get; set; } = int.MaxValue;
}
