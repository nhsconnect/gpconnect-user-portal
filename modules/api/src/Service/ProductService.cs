using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class ProductService : IProductService
{
    private readonly IDataService _dataService;

    public ProductService(IDataService dataService)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var query = "reference.get_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.PRODUCT, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQuery<Product>(query, parameters);
        return result;
    }

    public async Task<Product> GetProduct(int id)
    {
        var query = "reference.get_lookup_by_id";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.PRODUCT, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_id", id, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQueryFirstOrDefault<Product>(query, parameters);
        return result;
    }

    public async Task<IEnumerable<SupplierProduct>> GetProductsBySupplier(int supplierId)
    {
        var query = "reference.get_supplier_products";
        var parameters = new DynamicParameters();
        parameters.Add("_supplier_id", supplierId, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQuery<SupplierProduct>(query, parameters);
        return result;
    }

    public async Task<Product> AddProduct(ProductAddRequest productAddRequest)
    {
        var query = "reference.add_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.PRODUCT, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", productAddRequest.ProductValue, DbType.String, ParameterDirection.Input);
        parameters.Add("_linked_lookup_id", null, DbType.Int16, ParameterDirection.Input);
        return await _dataService.ExecuteQueryFirstOrDefault<Product>(query, parameters);
    }

    public async Task DisableProduct(ProductDisableRequest productDisableRequest)
    {
        var query = "reference.enable_disable_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", productDisableRequest.ProductId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.PRODUCT, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_is_disabled", productDisableRequest.ProductDisabled, DbType.Boolean, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<Product>(query, parameters);
    }

    public async Task UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        var query = "reference.update_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", productUpdateRequest.ProductId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.PRODUCT, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", productUpdateRequest.ProductValue, DbType.String, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<Product>(query, parameters);
    }
}
