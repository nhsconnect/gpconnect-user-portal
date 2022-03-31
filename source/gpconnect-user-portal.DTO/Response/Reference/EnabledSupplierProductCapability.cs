using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Reference
{
    public class EnabledSupplierProductCapability
    {
        public List<SupplierProductCapability> SupplierProductCapability { get; set; }

        public bool SendActionRequestEnabled => SupplierProductCapability.Any(x => x.CanSendActionRequest);

        public bool IsHtmlEnabled => SupplierProductCapability.Any(x => x.LookupValue.Contains("HTML"));
        public bool IsStructuredEnabled => SupplierProductCapability.Any(x => x.LookupValue.Contains("Structured"));
        public bool IsAppointmentEnabled => SupplierProductCapability.Any(x => x.LookupValue.Contains("Appointments"));
        public bool IsSendDocumentEnabled => SupplierProductCapability.Any(x => x.LookupValue.Contains("SendDocument"));

        public bool IsProviderAssuredForHtml => SupplierProductCapability.Any(x => x.LookupValue.Contains("HTML") && x.ProviderAssured);
        public bool IsConsumerAssuredForHtml => SupplierProductCapability.Any(x => x.LookupValue.Contains("HTML") && x.ConsumerAssured);
        public bool IsBothAssuredForHtml => IsProviderAssuredForHtml && IsConsumerAssuredForHtml;
        public bool IsNoneAssuredForHtml => !IsProviderAssuredForHtml && !IsConsumerAssuredForHtml;

        public bool IsProviderAssuredForStructured => SupplierProductCapability.Any(x => x.LookupValue.Contains("Structured") && x.ProviderAssured);
        public bool IsConsumerAssuredForStructured => SupplierProductCapability.Any(x => x.LookupValue.Contains("Structured") && x.ConsumerAssured);
        public bool IsBothAssuredForStructured => IsProviderAssuredForStructured && IsConsumerAssuredForStructured;
        public bool IsNoneAssuredForStructured => !IsProviderAssuredForStructured && !IsConsumerAssuredForStructured;

        public bool IsProviderAssuredForAppointment => SupplierProductCapability.Any(x => x.LookupValue.Contains("Appointments") && x.ProviderAssured);
        public bool IsConsumerAssuredForAppointment => SupplierProductCapability.Any(x => x.LookupValue.Contains("Appointments") && x.ConsumerAssured);
        public bool IsBothAssuredForAppointment => IsProviderAssuredForAppointment && IsConsumerAssuredForAppointment;
        public bool IsNoneAssuredForAppointment => !IsProviderAssuredForAppointment && !IsConsumerAssuredForAppointment;

        public bool IsProviderAssuredForSendDocument => SupplierProductCapability.Any(x => x.LookupValue.Contains("SendDocument") && x.ProviderAssured);
        public bool IsConsumerAssuredForSendDocument => SupplierProductCapability.Any(x => x.LookupValue.Contains("SendDocument") && x.ConsumerAssured);
        public bool IsBothAssuredForSendDocument => IsProviderAssuredForSendDocument && IsConsumerAssuredForSendDocument;
        public bool IsNoneAssuredForSendDocument => !IsProviderAssuredForSendDocument && !IsConsumerAssuredForSendDocument;
    }
}
