using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;

public enum SearchMode
{
    [QueryStringParameter("provider_name")]
    [DisplayParameter("Organisation Name")]
    Name,
    [QueryStringParameter("provider_code")]
    [DisplayParameter("Organisation ODS Code")]
    Code
}
