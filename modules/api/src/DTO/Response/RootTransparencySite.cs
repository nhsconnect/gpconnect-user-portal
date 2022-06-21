using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Response;

public class RootTransparencySite
{
    public int TransparencySiteCount { get; set; }
    public List<TransparencySite>? TransparencySites { get; set; } = null;
}
