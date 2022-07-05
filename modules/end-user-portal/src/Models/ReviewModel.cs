using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    public string SupplierName => _tempDataProviderService.GetItem<SoftwareSupplierResult>(TempDataConstants.SELECTEDSOFTWARESUPPLIERNAME)?.SoftwareSupplierName;
    public List<GpConnectInteractionForSupplier> GpConnectInteractionForSupplier => _tempDataProviderService.GetItem<List<GpConnectInteractionForSupplier>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER).Where(x => x.Selected).ToList();
    public OrganisationResult Organisation => _tempDataProviderService.GetItem<OrganisationResult>(TempDataConstants.ORGANISATION);
    public string SignatoryName => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYNAME);
    public string SignatoryRole => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYROLE);
    public string SignatoryEmail => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYEMAIL);
    public string UseCaseDescription => _tempDataProviderService.GetItem<string>(TempDataConstants.USECASEDESCRIPTION);

    public bool CanSubmitAgreement =>
        !string.IsNullOrWhiteSpace(SupplierName) &&
        Organisation != null &&
        GpConnectInteractionForSupplier?.Count > 0 &&
        !string.IsNullOrWhiteSpace(SignatoryName) &&
        !string.IsNullOrWhiteSpace(SignatoryRole) &&
        !string.IsNullOrWhiteSpace(SignatoryEmail) &&
        !string.IsNullOrWhiteSpace(UseCaseDescription);
}
