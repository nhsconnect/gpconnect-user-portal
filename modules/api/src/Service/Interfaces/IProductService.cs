using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task<IEnumerable<SupplierProduct>> GetProductsBySupplier(int supplierId);
    Task UpdateProduct(ProductUpdateRequest productUpdateRequest);
    Task DisableProduct(ProductDisableRequest productDisableRequest);
    Task<Product> AddProduct(ProductAddRequest productAddRequest);
}