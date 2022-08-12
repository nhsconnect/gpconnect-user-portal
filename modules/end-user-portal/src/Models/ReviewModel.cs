using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    public SoftwareSupplierResult SoftwareSupplier { get; set; }

    public List<GpConnectInteractions> SelectedGpConnectInteractionsForSupplier => _tempDataProviderService.GetItem<List<GpConnectInteractions>>(TempDataConstants.SELECTEDGPCONNECTINTERACTIONFORSUPPLIER).ToList().Select(i => i).ToList();

    public OrganisationResult Organisation { get; set; }


    public string SignatoryName => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYNAME);
    public string SignatoryRole => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYROLE);
    public string SignatoryEmail => _tempDataProviderService.GetItem<string>(TempDataConstants.SIGNATORYEMAIL);
    public string UseCaseDescription => _tempDataProviderService.GetItem<string>(TempDataConstants.USECASEDESCRIPTION);

    public bool CanSubmitAgreement =>
        SoftwareSupplier != null &&
        Organisation != null &&
        SelectedGpConnectInteractionsForSupplier.Count > 0 &&
        !string.IsNullOrWhiteSpace(SignatoryName) &&
        !string.IsNullOrWhiteSpace(SignatoryRole) &&
        !string.IsNullOrWhiteSpace(SignatoryEmail) &&
        !string.IsNullOrWhiteSpace(UseCaseDescription);
}
