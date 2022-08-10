using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.Api.Stores
{
    public class SupplierStore : ISupplierStore
    {
        private Dictionary<int, SupplierInteractions>? _supplierInteractionsData;
        private Dictionary<int, Supplier>? _supplierData;

        public Dictionary<int, Supplier> GetSupplierData()
        {
            if (_supplierData == null)
            {
                var supplierList = ReadDataFromFile<Supplier>();

                _supplierData = supplierList.ToDictionary(x => x.SupplierId, x => x);
            }

            return _supplierData;
        }

        public Dictionary<int, SupplierInteractions> GetSupplierInteractionsData()
        {
            if (_supplierInteractionsData == null)
            {
                var supplierInteractionList = ReadDataFromFile<SupplierInteractions>();

                _supplierInteractionsData = supplierInteractionList.ToDictionary(x => x.SupplierId, x => x);
            }

            return _supplierInteractionsData;
        }

        private List<T> ReadDataFromFile<T>() 
        {
            using (StreamReader r = File.OpenText("Stores/Suppliers.json"))
            {
                string json = r.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<T>>(json);
                
                if (data == null)
                {
                    throw new Exception("Failed to Load Suppliers Json");
                }

                return data;
            }
        }
    }
}