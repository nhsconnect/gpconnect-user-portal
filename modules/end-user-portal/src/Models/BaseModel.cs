using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models
{
  public abstract class BaseModel : PageModel
  {
    private readonly IOptions<ApplicationParameters> _applicationParameters;

    protected BaseModel(IOptions<ApplicationParameters> applicationParameters)
    {
      _applicationParameters = applicationParameters;
    }

    public string ProductName => _applicationParameters.Value.PRODUCT_NAME;
    public string ProductVersion => _applicationParameters.Value.PRODUCT_VERSION;
    public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";
  }
}
