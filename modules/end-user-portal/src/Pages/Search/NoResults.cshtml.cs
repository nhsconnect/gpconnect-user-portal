using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class NoResultsModel : BaseModel
{
    public NoResultsModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }

    public void OnGet()
    {
    }
}
