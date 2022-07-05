using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    public ReviewModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }

    public void OnGetAsync()
    {
        GetSignupData();
    }

    private void GetSignupData()
    {
        throw new NotImplementedException();
    }

    public IActionResult OnPost()
    {
        return Redirect("./Confirmation");
    }
}
