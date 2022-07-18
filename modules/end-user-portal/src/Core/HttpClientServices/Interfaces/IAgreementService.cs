namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface IAgreementService 
{
    Task SubmitAgreementAsync(
        string organisation, 
        string softwareSupplierName,
        List<int> interactions,
        string signatoryName, 
        string signatoryEmail, 
        string signatoryPosition, 
        string useCase
    );
}
