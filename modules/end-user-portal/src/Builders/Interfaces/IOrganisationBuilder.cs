using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces
{
    public interface IOrganisationBuilder
    {
        OrganisationInformation Build(OrganisationResult organisation);
    }
}