using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces
{
    public interface IOrganisationBuilder
    {
        Task<OrganisationInformation> Build(string organisationOdsCode);
    }
}
