using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Search;

public partial class NoResultsModel : BaseModel
{
  [BindProperty(SupportsGet=true)]
  public string Query { get; set; } = "";

  [BindProperty(SupportsGet=true)]
  public SearchMode Mode { get; set; } = SearchMode.Name;

  public string SearchType => Mode == SearchMode.Name ? "organisation name": "organisation ODS code";

  public string? NameQueryOrNull => Mode == SearchMode.Name ? Query : null;

  public string? CodeQueryOrNull => Mode == SearchMode.Code ? Query : null;
}
