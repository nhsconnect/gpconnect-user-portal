using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    public string SupplierName => _tempDataProviderService.GetItem<SoftwareSupplierResult>("SelectedSoftwareSupplierName")?.SoftwareSupplierName;
    public List<SoftwareSupplierProductResult> SupplierProducts => _tempDataProviderService.GetItem<List<SoftwareSupplierProductResult>>("SelectedSoftwareSupplierProduct");
    public OrganisationResult Organisation => _tempDataProviderService.GetItem<OrganisationResult>("Organisation");
    public string SignatoryName => _tempDataProviderService.GetItem<string>("SignatoryName");
    public string SignatoryRole => _tempDataProviderService.GetItem<string>("SignatoryRole");
    public string SignatoryEmail => _tempDataProviderService.GetItem<string>("SignatoryEmail");
    public string UseCaseDescription => _tempDataProviderService.GetItem<string>("UseCaseDescription");

    public bool CanSubmitAgreement => (SupplierName != null && Organisation != null && SignatoryName != null && SignatoryRole != null && SignatoryEmail != null && UseCaseDescription != null);
}
