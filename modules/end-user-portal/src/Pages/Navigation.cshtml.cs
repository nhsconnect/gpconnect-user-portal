using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages
{
  public class NavigationModel : BaseModel
    {
        private readonly ILogger<NavigationModel> _logger;

        public NavigationModel(ILogger<NavigationModel> logger, IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
