using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class SupplierProductMap : EntityMap<SupplierProduct>
    {
        public SupplierProductMap()
        {
            Map(p => p.SupplierName).ToColumn("supplier_name");
            Map(p => p.SupplierId).ToColumn("supplier_id");
            Map(p => p.SupplierProductId).ToColumn("supplier_product_id");
            Map(p => p.SupplierProductName).ToColumn("product_name");
        }
    }
}
