using gpconnect_user_portal.DAL.Resources;
using gpconnect_user_portal.DTO.Response.Reference;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class EndpointSupplierProductCapability
    {
        [Required(ErrorMessageResourceName = "RecordAccessHtmlView", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "RecordAccessHtmlView", ResourceType = typeof(DataFieldNameResources))]
        public string RecordAccessHtmlView { get; set; }

        [Required(ErrorMessageResourceName = "RecordAccessStructured", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "RecordAccessStructured", ResourceType = typeof(DataFieldNameResources))]
        public string RecordAccessStructured { get; set; }

        [Required(ErrorMessageResourceName = "Appointments", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "Appointments", ResourceType = typeof(DataFieldNameResources))]
        public string Appointments { get; set; }

        [Required(ErrorMessageResourceName = "SendDocument", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "SendDocument", ResourceType = typeof(DataFieldNameResources))]
        public string SendDocument { get; set; }

        [Required(ErrorMessageResourceName = "UseCaseDescription", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "UseCaseDescription", ResourceType = typeof(DataFieldNameResources))]
        public string UseCaseDescription { get; set; }

        public bool IsHtmlEnabled => EnabledSupplierProductCapability != null ? EnabledSupplierProductCapability.IsHtmlEnabled : false;
        public bool IsAppointmentEnabled => EnabledSupplierProductCapability != null ? EnabledSupplierProductCapability.IsAppointmentEnabled : false;
        public bool IsStructuredEnabled => EnabledSupplierProductCapability != null ? EnabledSupplierProductCapability.IsStructuredEnabled : false;
        public bool IsSendDocumentEnabled => EnabledSupplierProductCapability != null ? EnabledSupplierProductCapability.IsSendDocumentEnabled : false;

        public EnabledSupplierProductCapability EnabledSupplierProductCapability { get; set; }
    }
}
