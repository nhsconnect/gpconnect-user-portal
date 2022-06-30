using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SignatoryModel : BaseModel
{
    public SignatoryModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        TempData.Put("SignatoryName", SignatoryName);
        TempData.Put("SignatoryRole", SignatoryRole);
        TempData.Put("SignatoryEmail", SignatoryEmail);
        return Redirect("./UseCase");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SignatoryName");
        ModelState.ClearValidationState("SignatoryRole");
        ModelState.ClearValidationState("SignatoryEmail");
    }
}
