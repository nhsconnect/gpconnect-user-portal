using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models
{
    public class BaseModel : PageModel
    {
        private readonly IOptions<ApplicationParameters> _applicationParameters;

        public BaseModel(IOptions<ApplicationParameters> applicationParameters)
        {
            _applicationParameters = applicationParameters;
        }

        public string ProductName => _applicationParameters.Value.ProductName;
        public string ProductVersion => _applicationParameters.Value.ProductVersion;
        public string OwnerEmailAddress => _applicationParameters.Value.OwnerEmailAddress;
        public string OwnerTelephone => _applicationParameters.Value.OwnerTelephone;
    }
}
