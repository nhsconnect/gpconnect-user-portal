using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Stores
{
    public interface ISupplierStore 
    {
        public Dictionary<int, Supplier> GetSupplierData();
        public Dictionary<int, SupplierInteractions> GetSupplierInteractionsData();
    }
}