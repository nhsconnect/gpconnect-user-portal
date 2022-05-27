using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class ProductMap : EntityMap<Product>
    {
        public ProductMap()
        {
            Map(p => p.ProductId).ToColumn("lookup_id");
            Map(p => p.ProductValue).ToColumn("lookup_value");
            Map(p => p.ProductName).ToColumn("lookup_type_name");
            Map(p => p.ProductDescription).ToColumn("lookup_type_description");
        }
    }
}
