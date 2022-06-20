namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers
{
    public static class AddressBuilder
    {
        public static string GetFullAddress(List<string> addressLines, string town, string county, string postalCode, string country)
        {
            addressLines ??= new List<string>();
            addressLines.Add(town);
            addressLines.Add(county);
            addressLines.Add(postalCode);
            addressLines.Add(country);
            return string.Join(", ", addressLines.Where(s => !string.IsNullOrEmpty(s)));
        }
    }
}
