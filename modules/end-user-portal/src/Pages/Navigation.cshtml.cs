using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public class NavigationModel : BaseModel
{
  public NavigationModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
  {
  }

  public void OnGet()
  {
  }
}
