using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Request.Registration
{
    public class EnabledSupplierProductCapability
    {
        public List<SupplierProductCapability> SupplierProductCapability { get; set; }
        public bool IsHtmlEnabled { get; set; }
        public bool IsStructuredEnabled { get; set; }
        public bool IsAppointmentEnabled { get; set; }
        public bool IsProviderAssuredForHtml { get; set; }
        public bool IsConsumerAssuredForHtml { get; set; }
        public bool IsBothAssuredForHtml { get; set; }
        public bool IsNoneAssuredForHtml { get; set; }
        public bool IsProviderAssuredForStructured { get; set; }
        public bool IsConsumerAssuredForStructured { get; set; }
        public bool IsBothAssuredForStructured { get; set; }
        public bool IsNoneAssuredForStructured { get; set; }
        public bool IsProviderAssuredForAppointment { get; set; }
        public bool IsConsumerAssuredForAppointment { get; set; }
        public bool IsBothAssuredForAppointment { get; set; }
        public bool IsNoneAssuredForAppointment { get; set; }
    }
}