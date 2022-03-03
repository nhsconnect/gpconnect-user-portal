namespace gpconnect_user_portal.DTO.Response.Application.Search
{
    public class SiteAttributeValues
    {
        public string SiteName { get; set; }
        public string SelectedCCGOdsCode { get; set; }
        public string SelectedCCGName { get; set; }
        public bool IsAppointmentEnabled { get; set; }
        public bool IsHtmlEnabled { get; set; }
        public bool IsStructuredEnabled { get; set; }
        public bool IsSendDocumentEnabled { get; set; }
        public string SitePostcode { get; set; }
        public string OdsCode { get; set; }
        public string SelectedSupplier { get; set; }
        public string UseCaseDescription { get; set; }
    }
}
