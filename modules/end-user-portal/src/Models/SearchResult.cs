using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

public class SearchResult
{
    public List<SearchResultEntry> SearchResults { get; set; } = new List<SearchResultEntry>();

    [Display(Name = "MatchedCount", ResourceType = typeof(DataFieldNameResources))]
    public int? MatchedCount => SearchResults?.Count;

    [Display(Name = "HasHtmlView", ResourceType = typeof(DataFieldNameResources))]
    public int HasHtmlViewCount => SearchResults.Count(x => x.HasHtmlView);

    [Display(Name = "HasStructured", ResourceType = typeof(DataFieldNameResources))]
    public int HasStructuredCount => SearchResults.Count(x => x.HasStructured);

    [Display(Name = "HasAppointmentManagement", ResourceType = typeof(DataFieldNameResources))]
    public int HasAppointmentCount => SearchResults.Count(x => x.HasAppointmentManagement);

    [Display(Name = "HasSendDocument", ResourceType = typeof(DataFieldNameResources))]
    public int HasSendDocumentCount => SearchResults.Count(x => x.HasSendDocument);

    public bool NoMatches => SearchResults?.Count == 0;

    public int TotalResults => 300; //SearchResults.Count;
}
