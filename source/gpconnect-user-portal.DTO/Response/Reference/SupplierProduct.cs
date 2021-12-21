using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Reference
{
    public class EnabledSupplierProduct
    {
        public List<SupplierProduct> SupplierProduct { get; set; }
        public bool IsHtmlEnabled => SupplierProduct.Any(x => x.ProductName.Contains("HTML"));
        public bool IsStructuredEnabled => SupplierProduct.Any(x => x.ProductName.Contains("Structured"));
        public bool IsAppointmentEnabled => SupplierProduct.Any(x => x.ProductName.Contains("Appointment"));
    }

    public class SupplierProduct
    {
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public int SupplierProductId { get; set; }
        public string ProductName { get; set; }
        public int LookupTypeId { get; set; }
    }
}
