using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;

public class AgreementInformationBuilder: IAgreementInformationBuilder
{
    public AgreementInformation Build(OrganisationResult organisation, SoftwareSupplierResult supplier, string UseCase, List<GpConnectInteractionForSupplier> interactions, string SignatoryName, string SignatoryEmail, string SignatoryPosition)
    {
        return new AgreementInformation();
    }
}