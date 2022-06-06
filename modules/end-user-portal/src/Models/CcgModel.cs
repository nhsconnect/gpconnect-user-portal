using Microsoft.AspNetCore.Mvc;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class CcgModel
{
  [BindProperty(Name = "ccgOdsCode", SupportsGet = true)]
  public string CcgOdsCode { get; set; } = "";
  [BindProperty(Name = "ccgName", SupportsGet = true)]
  public string CcgName { get; set; } = "";
}
