using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages
{
  public class HeaderModel : BaseModel
    {
        private readonly ILogger<HeaderModel> _logger;

        public HeaderModel(ILogger<HeaderModel> logger, IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
