using gpconnect_user_portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class SupplierProductCapabilityDetailsModel
    {
        public int SupplierProductCapabilityId { get; set; }
        public int ProductCapabilityId { get; set; }
        public int SupplierProductId { get; set; }
        public int SupplierId { get; set; }

        public string? Name { get; set; }

        [Display(Name = "AssuranceDate", ResourceType = typeof(DataFieldNameResources))]
        public DateTime? AssuranceDate { get; set; }

        [Display(Name = "AwaitingAssurance", ResourceType = typeof(DataFieldNameResources))]
        public bool AwaitingAssurance { get; set; }

        [Display(Name = "Provider", ResourceType = typeof(DataFieldNameResources))]
        public bool ProviderAssured { get; set; }

        [Display(Name = "Consumer", ResourceType = typeof(DataFieldNameResources))]
        public bool ConsumerAssured { get; set; }

        [Display(Name = "Version", ResourceType = typeof(DataFieldNameResources))]
        public string? CapabilityVersion { get; set; }

        [Display(Name = "RecipientEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string? RecipientEmailAddress { get; set; }

        public bool CanSendActionRequest { get; set; }
    }
}