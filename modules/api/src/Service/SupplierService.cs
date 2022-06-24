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

public class SupplierService : ISupplierService
{
    private readonly IDataService _dataService;

    public SupplierService(IDataService dataService)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
    }

    public async Task<IEnumerable<Supplier>> GetSuppliers()
    {
        var query = "reference.get_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.SUPPLIER, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQuery<Supplier>(query, parameters);
        return result;
    }

    public async Task<Supplier> GetSupplier(int id)
    {
        var query = "reference.get_lookup_by_id";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.SUPPLIER, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_id", id, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQueryFirstOrDefault<Supplier>(query, parameters);
        return result;
    }

    public async Task<Supplier> AddSupplier(SupplierAddRequest supplierAddRequest)
    {
        var query = "reference.add_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.SUPPLIER, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", supplierAddRequest.SupplierValue, DbType.String, ParameterDirection.Input);
        parameters.Add("_linked_lookup_id", null, DbType.Int16, ParameterDirection.Input);
        return await _dataService.ExecuteQueryFirstOrDefault<Supplier>(query, parameters);
    }

    public async Task DisableSupplier(SupplierDisableRequest supplierDisableRequest)
    {
        var query = "reference.enable_disable_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", supplierDisableRequest.SupplierId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.SUPPLIER, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_is_disabled", supplierDisableRequest.SupplierDisabled, DbType.Boolean, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<Supplier>(query, parameters);
    }

    public async Task UpdateSupplier(SupplierUpdateRequest supplierUpdateRequest)
    {
        var query = "reference.update_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", supplierUpdateRequest.SupplierId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", Dal.Enumerations.LookupType.SUPPLIER, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", supplierUpdateRequest.SupplierValue, DbType.String, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<Supplier>(query, parameters);
    }
}
