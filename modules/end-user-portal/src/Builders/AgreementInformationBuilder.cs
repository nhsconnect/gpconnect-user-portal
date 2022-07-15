using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class AgreementInformationBuilder: IAgreementInformationBuilder
{
    private readonly IOrganisationBuilder _organisationBuilder;
    private readonly IInteractionsBuilder _interactionsBuilder;
    private readonly ISignatoryBuilder _signatoryBuilder;
    private readonly IOrganisationLookupService _organisationLookupService;

    public AgreementInformationBuilder(IOrganisationBuilder organisationBuilder, IInteractionsBuilder interactionsBuilder, ISignatoryBuilder signatoryBuilder, IOrganisationLookupService organisationLookupService)
    {
        _organisationBuilder = organisationBuilder;
        _interactionsBuilder = interactionsBuilder;
        _signatoryBuilder = signatoryBuilder;
        _organisationLookupService = organisationLookupService;
    }

    public async Task<AgreementInformation> Build(string organisationOdsCode, string supplierName, string useCase, List<int> interactions, string signatoryName, string signatoryEmail, string signatoryPosition)
    {
        return new AgreementInformation {
            Organisation = await _organisationBuilder.Build(organisationOdsCode),
            SoftwareSupplierName = supplierName,
            UseCase = useCase,
            Interactions = _interactionsBuilder.Build(interactions),
            Signatory = _signatoryBuilder.Build(signatoryName, signatoryEmail, signatoryPosition)
        };
    }
}
