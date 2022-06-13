using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public class IndexModel : BaseModel
{
  public IndexModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
  {
  }

  public void OnGet()
  {    
  }
}
