using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Request
{
    public class SearchRequest
    {
        public string ProviderOdsCode { get; set; }
        public string ProviderName { get; set; }
        public string CCGOdsCode { get; set; }
        public string CCGName { get; set; }
        public string FilterBy { get; set; }
        public List<string> ProviderOdsCodeAsList => ProviderOdsCode?.Split(',', ' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        public List<string> ProviderNameAsList => ProviderName?.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}
