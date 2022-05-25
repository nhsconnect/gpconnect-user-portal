using System.Collections.Generic;
using GpConnect.NationalDataSharingPortal.Api.DTO;
using GpConnect.NationalDataSharingPortal.Api.DTO.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Service
{
    public interface ITransparencySiteService
    {
        IEnumerable<TransparencySite> GetMatchingSites(TransparencySiteRequest request);
    }
}