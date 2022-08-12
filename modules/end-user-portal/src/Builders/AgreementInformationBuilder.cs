using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class AgreementInformationBuilder: IAgreementInformationBuilder
{
    private readonly IOrganisationBuilder _organisationBuilder;
    private readonly ISupplierBuilder _supplierBuilder;
    private readonly IInteractionsBuilder _interactionsBuilder;
    private readonly ISignatoryBuilder _signatoryBuilder;

    public AgreementInformationBuilder(IOrganisationBuilder organisationBuilder, ISupplierBuilder supplierBuilder, IInteractionsBuilder interactionsBuilder, ISignatoryBuilder signatoryBuilder)
    {
        _organisationBuilder = organisationBuilder;
        _supplierBuilder = supplierBuilder;
        _interactionsBuilder = interactionsBuilder;
        _signatoryBuilder = signatoryBuilder;
    }

    public async Task<AgreementInformation> Build(string organisationOdsCode, string supplierId, string useCase, List<Helpers.Constants.GpConnectInteractions> interactions, string signatoryName, string signatoryEmail, string signatoryPosition)
    {
        return new AgreementInformation {
            Organisation = await _organisationBuilder.Build(organisationOdsCode),
            SoftwareSupplier = await _supplierBuilder.Build(int.Parse(supplierId)),
            UseCase = useCase,
            Interactions = _interactionsBuilder.Build(interactions),
            Signatory = _signatoryBuilder.Build(signatoryName, signatoryEmail, signatoryPosition)
        };
    }
}
