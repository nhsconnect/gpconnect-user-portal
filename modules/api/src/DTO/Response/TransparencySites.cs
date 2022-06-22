using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Response;

public class TransparencySites
{
    public int TotalResults { get; set; } = 0;
    public List<TransparencySite> Results { get; set; } = new List<TransparencySite>();
}
