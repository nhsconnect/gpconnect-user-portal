using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class AgreementInformationBuilder: IAgreementInformationBuilder
{
    private readonly IOrganisationBuilder _organisationBuilder;
    private readonly IInteractionsBuilder _interactionsBuilder;
    private readonly ISignatoryBuilder _signatoryBuilder;

    public AgreementInformationBuilder(IOrganisationBuilder organisationBuilder, IInteractionsBuilder interactionsBuilder, ISignatoryBuilder signatoryBuilder)
    {
        _organisationBuilder = organisationBuilder;
        _interactionsBuilder = interactionsBuilder;
        _signatoryBuilder = signatoryBuilder;
    }

    public AgreementInformation Build(OrganisationResult organisation, SoftwareSupplierResult supplier, string useCase, List<GpConnectInteractionForSupplier> interactions, string signatoryName, string signatoryEmail, string signatoryPosition)
    {
        return new AgreementInformation {
            Organisation = _organisationBuilder.Build(organisation),
            SoftwareSupplierName = supplier.SoftwareSupplierName,
            UseCase = useCase,
            Interactions = _interactionsBuilder.Build(interactions),
            Signatory = _signatoryBuilder.Build(signatoryName, signatoryEmail, signatoryPosition)
        };
    }
}