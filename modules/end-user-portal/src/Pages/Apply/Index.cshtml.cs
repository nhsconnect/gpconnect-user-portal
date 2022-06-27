using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public class IndexModel : BaseModel
{
  public IndexModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
  {
  }
}
