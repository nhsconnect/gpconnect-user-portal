using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class SignatoryModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;

    public SignatoryModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");
        PrepopulateSignatoryDetails();
        return Page();
    }

    private void PrepopulateSignatoryDetails()
    {
        SignatoryName = _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYNAME) ?? string.Empty;
        SignatoryRole = _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYROLE) ?? string.Empty;
        SignatoryEmail = _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYEMAIL) ?? string.Empty;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        _tempDataProviderService.PutItem(TempDataConstants.SIGNATORYNAME, SignatoryName);
        _tempDataProviderService.PutItem(TempDataConstants.SIGNATORYROLE, SignatoryRole);
        _tempDataProviderService.PutItem(TempDataConstants.SIGNATORYEMAIL, SignatoryEmail);
        return RedirectToPage("./UseCase");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SignatoryName");
        ModelState.ClearValidationState("SignatoryRole");
        ModelState.ClearValidationState("SignatoryEmail");
    }
}
