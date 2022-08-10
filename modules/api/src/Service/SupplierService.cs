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

public class SupplierService : ISupplierService
{
    private readonly ISupplierStore _supplierStore;

    public SupplierService(ISupplierStore supplierStore)
    {
        _supplierStore = supplierStore;
    }

    public Task<IEnumerable<Supplier>> GetSuppliers()
    {
        return Task.FromResult((IEnumerable<Supplier>)_supplierStore.GetSupplierData().Values);
    }

    public Task<Supplier?> GetSupplier(int id)
    {
        return Task.FromResult(_supplierStore.GetSupplierData().GetValueOrDefault(id));
    }

    public async Task<Supplier> AddSupplier(SupplierAddRequest supplierAddRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DisableSupplier(SupplierDisableRequest supplierDisableRequest)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSupplier(SupplierUpdateRequest supplierUpdateRequest)
    {
        throw new NotImplementedException();
    }

}
