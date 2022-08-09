using GpConnect.NationalDataSharingPortal.Api.Stores;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Store
{
    public class SupplierStoreTest
    {

        [Fact]
        public void GetSupplierData_ReturnsExpectedData()
        {
            // Given
            var supplierStore = new SupplierStore();

            // When
            var result = supplierStore.GetSupplierData();

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSupplierInteractionsData_ReturnsExpectedData()
        {
            // Given
            var supplierStore = new SupplierStore();

            // When
            var result = supplierStore.GetSupplierInteractionsData();

            // Then
            Assert.NotNull(result);
        }
    }
}