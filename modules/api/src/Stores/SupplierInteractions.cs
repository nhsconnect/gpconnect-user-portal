using System.Collections.Generic;

namespace GpConnect.NationalDataSharingPortal.Api.Stores
{
    public class SupplierInteractions
    {
        public int SupplierId { get; set; }
        public List<string>? Interactions { get; set; }
    }
}