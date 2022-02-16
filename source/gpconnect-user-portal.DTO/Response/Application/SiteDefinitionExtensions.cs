using gpconnect_user_portal.DTO.Response.Application.Search;
using System.Linq;

namespace gpconnect_user_portal.Helpers
{
    public static class SearchResultEntryExtensions
    {
        public static string GetSiteAttribute(this SearchResultEntry searchResultEntry, string siteAttributeName) =>
            searchResultEntry switch
            {
                null => string.Empty,
                _ => searchResultEntry.SiteAttributesDictionary.FirstOrDefault(x => x.Key == siteAttributeName).Value
            };
    }
}
