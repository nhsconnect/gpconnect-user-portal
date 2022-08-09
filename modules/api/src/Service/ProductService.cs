using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class ProductService : IProductService
{
    public ProductService()
    {
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SupplierProduct>> GetProductsBySupplier(int supplierId)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> AddProduct(ProductAddRequest productAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DisableProduct(ProductDisableRequest productDisableRequest)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        throw new NotImplementedException();
    }
}
