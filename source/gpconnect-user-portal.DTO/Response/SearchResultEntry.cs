namespace gpconnect_user_portal.DTO.Response
{
    public class SearchResultEntry
    {
        public string SiteName { get; set; }
        public string SiteODSCode { get; set; }
        public string CCGName { get; set; }
        public string CCGODSCode { get; set; }
        public bool HasHtmlView { get; set; }
        public bool HasStructured { get; set; }
        public bool HasAppointment { get; set; }
        public string UseCase { get; set; }
    }
}
