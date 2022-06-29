using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public class AgreementModel : BaseModel
{
    public AgreementModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {        
    }
}
