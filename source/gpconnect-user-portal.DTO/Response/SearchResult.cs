using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response
{
    public class SearchResult
    {
        public List<SearchResultEntry> SearchResults { get; set; }

        public int MatchedCount => SearchResults.Count;
        public int HasHtmlViewCount => SearchResults.Count(x => x.HasHtmlView);
        public int HasStructuredCount => SearchResults.Count(x => x.HasStructured);
        public int HasAppointmentCount => SearchResults.Count(x => x.HasAppointment);
    }
}
