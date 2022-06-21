using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface ITransparencySiteService
{
    Task<RootTransparencySite> GetMatchingSitesAsync(TransparencySiteRequest request);

    Task<TransparencySite> GetSiteAsync(Guid id);
}
