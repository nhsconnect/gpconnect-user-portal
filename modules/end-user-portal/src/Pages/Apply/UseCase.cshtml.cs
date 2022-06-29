using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
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
        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return Redirect("./Agreement");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("UseCaseDescription");
    }
}
