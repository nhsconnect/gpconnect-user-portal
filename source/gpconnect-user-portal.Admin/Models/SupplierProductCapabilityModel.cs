namespace gpconnect_user_portal.Models
{
    public class SupplierProductCapabilityModel
    {
        public SupplierProductCapabilityModel()
        {
            SupplierProductCapabilitySupplierModel = new SupplierProductCapabilitySupplierModel();
            SupplierProductCapabilityDetailsModel = new List<SupplierProductCapabilityDetailsModel>();
        }

        public SupplierProductCapabilitySupplierModel SupplierProductCapabilitySupplierModel { get; set; }
        public List<SupplierProductCapabilityDetailsModel>? SupplierProductCapabilityDetailsModel { get; set; }

        public bool DisplayCapabilities { get; set; }

        public bool DisplaySupplierProducts { get; set; }
    }
}