namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface IAgreementService 
{
    Task SubmitAgreementAsync(
        string organisationOdsCode, 
        string supplierName, 
        List<Helpers.Constants.GpConnectInteractions> interactions,
        string signatoryName, 
        string signatoryEmail, 
        string signatoryPosition, 
        string useCase
    );
}
