using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;

public interface ISupplierBuilder
{
    Task<SupplierInformation> Build(int supplierId);
}
