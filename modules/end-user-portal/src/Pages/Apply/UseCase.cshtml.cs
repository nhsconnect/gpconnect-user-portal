using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class UseCaseModel : BaseModel
{
    public UseCaseModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        UseCaseDescription = TempData.Get<string>("UseCaseDescription");
        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        TempData.Put("UseCaseDescription", UseCaseDescription);
        return Redirect("./Agreement");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("UseCaseDescription");
    }
}
