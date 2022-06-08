using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public class IndexModel : BaseModel
{
  public IndexModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
  {
  }

  public RedirectResult OnGet()
  {
    return Redirect("/Search");
  }
}
