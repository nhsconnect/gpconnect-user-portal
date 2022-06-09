using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class TransparencySiteRequest
{
  [BindProperty(Name = "provider_code", SupportsGet = true)]
  public string? ProviderCode { get; set; }

  public List<string>? ProviderCodeAsList => ProviderCode?.Split(',', ' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

  [BindProperty(Name = "provider_name", SupportsGet = true)]
  public string? ProviderName { get; set; }

  public List<string>? ProviderNameAsList => ProviderName?.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

  [BindProperty(Name = "ccg_code", SupportsGet = true)]
  public string? CcgCode { get; set; }

  [BindProperty(Name = "ccg_name", SupportsGet = true)]
  public string? CcgName { get; set; }
}
