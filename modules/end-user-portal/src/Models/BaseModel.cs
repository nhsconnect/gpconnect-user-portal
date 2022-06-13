using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using Microsoft.AspNetCore.Html;
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

        public string ProductName => _applicationParameters.Value.ProductName;
        public string ProductVersion => _applicationParameters.Value.ProductVersion;
        public HtmlString OwnerEmailAddress => _applicationParameters.Value.OwnerEmailAddress.CreateHtmlString(Helpers.Enumerations.LinkTypeEnums.MailTo);
        public HtmlString OwnerTelephone => _applicationParameters.Value.OwnerTelephone.CreateHtmlString(Helpers.Enumerations.LinkTypeEnums.Telephone);
        public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";
    }
}
