using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;

public interface IAgreementInformationBuilder
{
    public Task<AgreementInformation> Build(string organisation, string supplier, string UseCase, List<Helpers.Constants.GpConnectInteractions> interactions, string signatoryName, string signatoryEmail, string signatoryPosition);
}   

