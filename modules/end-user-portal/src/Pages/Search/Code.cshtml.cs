using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class SearchByCodeModel : BaseModel
{
    public SearchByCodeModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {        
    }

    public IActionResult OnGet()
    {
        ClearModelState();
        return Page();
    }

    public IActionResult OnPostSearchAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return RedirectToPage("./Results", new { query = ProviderOdsCode, mode = SearchMode.Code });
    }

    public IActionResult OnPostClear()
    {
        ProviderOdsCode = null;
        ModelState.Clear();
        return Page();
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("ProviderOdsCode");
    }
}
