using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Threading.Tasks;

using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class ProductRequestValidator : IProductRequestValidator
{
    private readonly IProductService _productService;

    public ProductRequestValidator(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public async Task<BaseRequestValidator> IsValidUpdate(ProductUpdateRequest request) 
    {
        var validParameters = request.ProductId > 0 && (!string.IsNullOrWhiteSpace(request.ProductValue));
        var product = await _productService.GetProduct(request.ProductId);
        return new BaseRequestValidator() { EntityFound = product != null, RequestValid = validParameters };
    }

    public async Task<BaseRequestValidator> IsValidDisable(ProductDisableRequest request)
    {
        var validParameters = request.ProductId > 0;
        var product = await _productService.GetProduct(request.ProductId);
        return new BaseRequestValidator() { EntityFound = product != null, RequestValid = validParameters };
    }

    public bool IsValidAdd(ProductAddRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.ProductValue);
    }
}
