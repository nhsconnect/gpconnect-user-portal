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
    private readonly ISupplierService _supplierService;
    private readonly IOrganisationLookupService _organisationLookupService;

    public ReviewModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService, IOrganisationLookupService organisationLookupService, IAgreementService agreementService, ISupplierService supplierService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
        _agreementService = agreementService;
        _supplierService = supplierService;
        _organisationLookupService = organisationLookupService;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        var supplier = await _supplierService.GetSoftwareSupplierAsync(int.Parse(_tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDSOFTWARESUPPLIERID)));
        var organisation = await _organisationLookupService.GetOrganisationAsync(_tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDORGANISATIONODSCODE));

        SoftwareSupplier = supplier;
        Organisation = organisation;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        await _agreementService.SubmitAgreementAsync(
            _tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDORGANISATIONODSCODE),
            _tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDSOFTWARESUPPLIERID),
            _tempDataProviderService.GetItem<List<GpConnectInteractions>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER),
            SignatoryName,
            SignatoryEmail,
            SignatoryRole,
            UseCaseDescription);

        _tempDataProviderService.RemoveAll();
        return RedirectToPage("./Confirmation");
    }
}
