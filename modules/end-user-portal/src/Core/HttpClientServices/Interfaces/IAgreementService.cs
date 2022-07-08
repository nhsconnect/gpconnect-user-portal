using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface IAgreementService 
{
    Task SubmitAgreementAsync(
        OrganisationResult organisation, 
        SoftwareSupplierResult supplier, 
        List<GpConnectInteractionForSupplier> interactions,
        string signatoryName, 
        string signatoryEmail, 
        string signatoryPosition, 
        string useCase
    );
}