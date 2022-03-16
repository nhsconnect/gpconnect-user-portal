using gpconnect_user_portal.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Models
{
    public class SupplierProductCapabilitySupplierModel
    {
        [Display(Name = "SelectedSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
        public IEnumerable<SelectListItem>? SupplierProducts { get; set; }

        [Display(Name = "SelectedSupplier", ResourceType = typeof(DataFieldNameResources))]
        public IEnumerable<SelectListItem>? Suppliers { get; set; }

        [Display(Name = "SelectedSupplier", ResourceType = typeof(DataFieldNameResources))]
        public int SelectedSupplier { get; set; }

        [Display(Name = "SelectedSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
        public int? SelectedSupplierProduct { get; set; }

        public bool DisplayGpConnectProducts => SelectedSupplier >= 0;
    }
}
