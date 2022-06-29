using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;


public interface IOrganizationLookupService
{
    Task<Organization> GetOrganizationAsync(string odsCode);
}
