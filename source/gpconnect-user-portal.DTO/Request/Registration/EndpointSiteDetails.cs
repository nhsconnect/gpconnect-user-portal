namespace gpconnect_user_portal.DTO.Request.Registration
{
    public class EndpointSiteDetails
    {
        public string SiteName { get; set; }
        public string SitePostcode { get; set; }
        public string OdsCode { get; set; }
        public bool NoOdsIssued { get; set; }
        public int? SelectedCCGName { get; set; }
        public int? SelectedCCGOdsCode { get; set; }
    }    
}