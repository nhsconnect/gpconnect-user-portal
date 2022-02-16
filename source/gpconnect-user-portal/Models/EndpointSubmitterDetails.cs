using gpconnect_user_portal.DAL.Resources;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class EndpointSubmitterDetails
    {
        [Required(ErrorMessageResourceName = "ContactName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "ContactName", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactName { get; set; }

        [Required(ErrorMessageResourceName = "ContactEmailAddress", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "ContactEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactEmailAddress { get; set; }

        [Required(ErrorMessageResourceName = "ContactTelephone", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "ContactTelephone", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactTelephone { get; set; }
    }
}
