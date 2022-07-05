using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;

    public ReviewModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
    }

    public IActionResult OnGet()
    {
        if (!_tempDataProviderService.HasItems)
        {
            return RedirectToPage("./Timeout");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!_tempDataProviderService.HasItems)
        {
            return RedirectToPage("./Timeout");
        }
        _tempDataProviderService.RemoveAll();
        return RedirectToPage("./Confirmation");
    }
}
