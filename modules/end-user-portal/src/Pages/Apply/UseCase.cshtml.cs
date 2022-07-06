using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class UseCaseModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;

    public UseCaseModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");
        PrepopulateUseCaseDetails();
        return Page();
    }

    private void PrepopulateUseCaseDetails()
    {
        UseCaseDescription = _tempDataProviderService.GetItem<string>(TempDataConstants.USECASEDESCRIPTION) ?? string.Empty;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        _tempDataProviderService.PutItem(TempDataConstants.USECASEDESCRIPTION, UseCaseDescription);
        return RedirectToPage("./Agreement");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("UseCaseDescription");
    }
}
