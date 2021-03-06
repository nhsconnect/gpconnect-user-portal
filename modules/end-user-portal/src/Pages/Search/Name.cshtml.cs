using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class SearchByNameModel : BaseModel
{
    public SearchByNameModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
    }

    public IActionResult OnGet()
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
        return RedirectToPage("./Results", new { query = ProviderName, mode = SearchMode.Name });
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("ProviderName");
    }
}
