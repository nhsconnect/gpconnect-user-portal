using gpconnect_user_portal.Helpers;
using System;

namespace gpconnect_user_portal.DTO.Response.Application
{
    public class EndpointChange
    {
        public DateTime SubmittedDate { get; set; }
        public string SubmittedDateText => GetSubmittedDateText();

        private string GetSubmittedDateText()
        {
            var days = (DateTime.Today - SubmittedDate.Date).Days;
            return days == 0 ? "Today" : StringExtensions.Pluraliser("{0} day{1} ago", days);
        }

        public string SiteDefinitionStatusName { get; set; }
        public int SiteDefinitionId { get; set; }
        public Guid SiteUniqueIdentifier { get; set; }
        public int SiteDefinitionStatusId { get; set; }
        public string SiteName { get; set; }
    }
}
