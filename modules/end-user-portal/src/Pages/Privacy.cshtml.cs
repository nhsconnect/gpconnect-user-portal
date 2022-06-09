using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public class PrivacyModel : BaseModel
{
  public PrivacyModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
  {
  }

  public void OnGet()
  {
  }
}
