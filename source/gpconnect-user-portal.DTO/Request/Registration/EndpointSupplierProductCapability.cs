namespace gpconnect_user_portal.DTO.Request.Registration
{
    public class EndpointSupplierProductCapability
    {
        public string RecordAccessHtmlView { get; set; }
        public string RecordAccessStructured { get; set; }
        public string Appointments { get; set; }
        public string SendDocument { get; set; }
        public string UseCaseDescription { get; set; }
        public bool IsHtmlEnabled { get; set; }
        public bool IsStructuredEnabled { get; set; }
        public bool IsAppointmentEnabled { get; set; }
        public bool IsSendDocumentEnabled { get; set; }
    }
}