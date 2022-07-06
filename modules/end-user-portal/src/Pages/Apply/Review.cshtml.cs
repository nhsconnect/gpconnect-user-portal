using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;
    private readonly IAgreementService _agreementService;

    public ReviewModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService, IAgreementService agreementService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
        _agreementService = agreementService;
    }

    public IActionResult OnGet()
    {
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        var supplier = _tempDataProviderService.GetItem<SoftwareSupplierResult>(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAME);
        await _agreementService.SubmitAgreementAsync(Organisation, supplier, GpConnectInteractionForSupplier, SignatoryName, SignatoryEmail, SignatoryRole, UseCaseDescription);

        _tempDataProviderService.RemoveAll();
        return RedirectToPage("./Confirmation");
    }
}
