using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class OrganisationBuilder : IOrganisationBuilder
{
    private readonly IOrganisationLookupService _organisationLookupService;

    public OrganisationBuilder(IOrganisationLookupService organisationLookupService)
    {
        _organisationLookupService = organisationLookupService;
    }

    public async Task<OrganisationInformation> Build(string organisationOdsCode)
    {
        var organisation = await _organisationLookupService.GetOrganisationAsync(organisationOdsCode);

        return new OrganisationInformation
        {
            OdsCode = organisation.OdsCode,
            Name = organisation.Name,
            AddressLine1 = organisation.Address.AddressLines[0],
            AddressLine2 = organisation.Address.AddressLines.Count > 1 ? organisation.Address.AddressLines[1] : string.Empty,
            Town = organisation.Address.City,
            County = organisation.Address.County,
            Country = organisation.Address.Country,
            PostCode = organisation.Address.Postcode
        };
    }
}
