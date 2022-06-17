using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public class BackPartialModel
{
    public string Query { get; set; }
    public SearchMode Mode { get; set; } = SearchMode.Name;
    public DetailViewSource Source { get; set; } = DetailViewSource.Search;
}
