using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface IOrganisationLookupService
{
    Task<OrganisationResult> GetOrganisationAsync(string odsCode);
}
