using gpconnect_user_portal.DAL.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class EndpointSupplierDetails
    {
        [Display(Name = "SelectedCareSetting", ResourceType = typeof(DataFieldNameResources))]
        [Required(ErrorMessageResourceName = "SelectedCareSetting", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        public int SelectedCareSetting { get; set; }

        [Display(Name = "SelectedSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
        [Required(ErrorMessageResourceName = "SelectedSupplierProduct", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        public int SelectedSupplier { get; set; }

        public bool DisplayGpConnectProducts { get; set; }

        [Display(Name = "SelectedCareSetting", ResourceType = typeof(DataFieldNameResources))]
        public IEnumerable<SelectListItem> CareSettings { get; set; }

        [Display(Name = "SelectedSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
        public IEnumerable<SelectListItem> SupplierProducts { get; set; }
    }
}
