using gpconnect_user_portal.Helpers.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Application.Search
{
    public class SearchResult
    {
        public List<SearchResultEntry> SearchResultEntries { get; set; }

        [Display(Name = DisplayConstants.MATCHEDCOUNT)]
        public int MatchedCount => SearchResultEntries.Count;

        [Display(Name = DisplayConstants.HASHTMLVIEW)]
        public int HasHtmlViewCount => SearchResultEntries.Count(x => x.HasHtmlView);
        
        [Display(Name = DisplayConstants.HASSTRUCTURED)]
        public int HasStructuredCount => SearchResultEntries.Count(x => x.HasStructured);
        
        [Display(Name = DisplayConstants.HASAPPOINTMENT)]
        public int HasAppointmentCount => SearchResultEntries.Count(x => x.HasAppointment);
        
        public bool HasNoMatches => SearchResultEntries.Count == 0;
        
        public bool HasSingleMatch => SearchResultEntries.Count == 1;
        
        public string SearchResultsHeading => (HasNoMatches || HasSingleMatch) ? "Results" : "Select site to modify";
    }
}
