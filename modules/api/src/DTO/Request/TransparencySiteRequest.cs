using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class TransparencySiteRequest
{
    [BindProperty(Name = "provider_code", SupportsGet = true)]
    public string? ProviderCode { get; set; }

    [BindProperty(Name = "provider_name", SupportsGet = true)]
    public string? ProviderName { get; set; }

    [BindProperty(Name = "ccg_code", SupportsGet = true)]
    public string? CcgCode { get; set; }

    [BindProperty(Name = "ccg_name", SupportsGet = true)]
    public string? CcgName { get; set; }
}
