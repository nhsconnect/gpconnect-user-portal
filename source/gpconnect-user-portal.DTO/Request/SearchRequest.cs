using gpconnect_user_portal.Helpers.Constants;
using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Request
{
    public class SearchRequest
    {
        public string SiteOdsCode { get; set; }
        public string SiteName { get; set; }
        public string CCGOdsCode { get; set; }
        public string CCGName { get; set; }
        public string FilterBy { get; set; }

        public string SiteNameAttributeName => SearchConstants.SiteNameAttributeName;
        public string CCGOdsCodeAttributeName => SearchConstants.CCGOdsCodeAttributeName;
        public string CCGNameAttributeName => SearchConstants.CCGOdsCodeAttributeName;

        public List<string> SiteOdsCodeAsList => SiteOdsCode?.Split(',', ' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        public List<string> SiteNameAsList => SiteName?.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
