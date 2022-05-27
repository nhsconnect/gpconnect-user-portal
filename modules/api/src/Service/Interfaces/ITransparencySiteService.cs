using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface ITransparencySiteService
{
    IEnumerable<TransparencySite> GetMatchingSites(TransparencySiteRequest request);
}