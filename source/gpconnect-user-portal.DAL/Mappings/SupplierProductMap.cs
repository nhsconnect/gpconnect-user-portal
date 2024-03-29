﻿using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SupplierProductMap : EntityMap<SupplierProducts>
    {
        public SupplierProductMap()
        {
            Map(p => p.SupplierId).ToColumn("supplier_id");
            Map(p => p.SupplierProductId).ToColumn("supplier_product_id");
            Map(p => p.ProductName).ToColumn("product_name");
        }
    }
}