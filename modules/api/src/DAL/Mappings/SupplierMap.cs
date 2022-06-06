using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class SupplierMap : EntityMap<Supplier>
    {
        public SupplierMap()
        {
            Map(p => p.SupplierId).ToColumn("lookup_id");
            Map(p => p.SupplierValue).ToColumn("lookup_value");
            Map(p => p.SupplierName).ToColumn("lookup_type_name");
            Map(p => p.SupplierDescription).ToColumn("lookup_type_description");
        }
    }
}
