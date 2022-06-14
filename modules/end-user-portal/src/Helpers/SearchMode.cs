using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

public enum SearchMode
{
    [QueryStringParameter("provider_name")]
    [NameParameter("Organisation Name")]
    Name,
    [QueryStringParameter("provider_code")]
    [NameParameter("Organisation ODS Code")]
    Code
}
