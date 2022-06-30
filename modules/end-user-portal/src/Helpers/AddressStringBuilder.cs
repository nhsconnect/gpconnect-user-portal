using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers
{
    public static class AddressStringBuilder
    {
        public static string GetFullAddressAsString(this SearchResultEntry entry)
        {
            var addressLines = new List<string> {
                entry.SiteAddressLine1, 
                entry.SiteAddressLine2,
                entry.SiteAddressTown,
                entry.SiteAddressCounty,
                entry.SitePostcode,
                entry.SiteAddressCountry
            };
            
            return string.Join(", ", addressLines.Where(s => !string.IsNullOrEmpty(s)));
        }
    }
}
