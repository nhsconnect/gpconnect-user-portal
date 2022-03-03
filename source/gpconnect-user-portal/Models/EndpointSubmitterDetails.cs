using gpconnect_user_portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class EndpointSubmitterDetails
    {
        [Required(ErrorMessageResourceName = "ContactName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "SubmitterContactName", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactName { get; set; }

        [Required(ErrorMessageResourceName = "ContactEmailAddress", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "SubmitterContactEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactEmailAddress { get; set; }

        [Required(ErrorMessageResourceName = "ContactTelephone", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "SubmitterContactTelephone", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactTelephone { get; set; }
    }
}
