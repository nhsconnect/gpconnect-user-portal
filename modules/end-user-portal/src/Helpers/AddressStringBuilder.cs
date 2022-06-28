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

        // Feels like display logic in Response object... move to model?
    // public string FullAddressOnly => AddressBuilder.GetFullAddress(new List<string>() {  }, SiteAddressTown, SiteAddressCounty, SitePostcode, SiteAddressCountry);

    // public string SiteNameWithAddress => AddressBuilder.GetFullAddress(new List<string>() { SiteName, SiteAddressLine1, SiteAddressLine2 }, SiteAddressTown, SiteAddressCounty, SitePostcode, SiteAddressCountry);
    }
}
