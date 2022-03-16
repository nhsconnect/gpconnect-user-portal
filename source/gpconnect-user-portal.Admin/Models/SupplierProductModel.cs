using gpconnect_user_portal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierProductModel : BaseSiteModel
    {
        [Display(Name = "SelectedSupplier", ResourceType = typeof(DataFieldNameResources))]
        public string SupplierName { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "SelectedSupplier", ResourceType = typeof(DataFieldNameResources))]
        public int SelectedSupplier { get; set; }

        [Required(ErrorMessageResourceName = "ProductName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "ProductName", ResourceType = typeof(DataFieldNameResources))]
        public string ProductName { get; set; }

        [Required(ErrorMessageResourceName = "ProductUseCase", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "ProductUseCase", ResourceType = typeof(DataFieldNameResources))]
        public string ProductUseCase { get; set; }
    }
}