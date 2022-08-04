using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models
{
    public class BaseModel : PageModel
    {
        private readonly IOptions<ApplicationParameters> _applicationParameters;

        public BaseModel(IOptions<ApplicationParameters> applicationParameters)
        {
            _applicationParameters = applicationParameters;
        }

        [Display(Name = "TransparencyPageTitle", ResourceType = typeof(HeadingResources))]
        public string TransparencyPageTitle { get; set; }

        [Display(Name = "SignupPageTitle", ResourceType = typeof(HeadingResources))]
        public string SignupPageTitle { get; set; }

        public string ProductName => _applicationParameters.Value.ProductName;
        public string ProductNameAbbreviated => _applicationParameters.Value.ProductNameAbbreviated;
        public string ProductVersion => _applicationParameters.Value.ProductVersion;
        public string OwnerEmailAddress => _applicationParameters.Value.OwnerEmailAddress;
        public string OwnerTelephone => _applicationParameters.Value.OwnerTelephone;
    }
}
