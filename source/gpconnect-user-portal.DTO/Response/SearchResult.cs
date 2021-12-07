using gpconnect_user_portal.Helpers.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response
{
    public class SearchResult
    {
        public List<SearchResultEntry> SearchResults { get; set; }

        [Display(Name = DisplayConstants.MATCHEDCOUNT)]
        public int MatchedCount => SearchResults.Count;
        [Display(Name = DisplayConstants.HASHTMLVIEW)]
        public int HasHtmlViewCount => SearchResults.Count(x => x.HasHtmlView);
        [Display(Name = DisplayConstants.HASSTRUCTURED)]
        public int HasStructuredCount => SearchResults.Count(x => x.HasStructured);
        [Display(Name = DisplayConstants.HASAPPOINTMENT)]
        public int HasAppointmentCount => SearchResults.Count(x => x.HasAppointment);
        public bool HasNoMatches => SearchResults.Count == 0;
        public bool HasSingleMatch => SearchResults.Count == 1;
        public string SearchResultsHeading => (HasNoMatches || HasSingleMatch) ? "Results" : "Select site to modify";

        public int SearchResultsSelectionHeight => SearchResults.Count > 10 ? 10 : SearchResults.Count;
    }
}
