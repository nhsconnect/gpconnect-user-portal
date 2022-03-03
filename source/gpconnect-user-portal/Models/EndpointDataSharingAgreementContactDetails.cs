using gpconnect_user_portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class EndpointDataSharingAgreementContactDetails
    {
        [Required(ErrorMessageResourceName = "ContactName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "DataSharingAgreementContactName", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactName { get; set; }

        [Required(ErrorMessageResourceName = "ContactEmailAddress", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "DataSharingAgreementContactEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactEmailAddress { get; set; }

        [Required(ErrorMessageResourceName = "ContactTelephone", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "DataSharingAgreementContactTelephone", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactTelephone { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessageResourceName = "DataSharingAgreementConfirmation", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "DataSharingAgreementConfirmation", ResourceType = typeof(DataFieldNameResources))]
        public bool DataSharingAgreementConfirmation { get; set; }
    }
}
