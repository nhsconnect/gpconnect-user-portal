using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRequestValidator _validator;
    private readonly IProductService _service;

    public ProductController(IProductRequestValidator validator, IProductService service, ILogger<ProductController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _validator = validator ?? throw new ArgumentNullException();
        _service = service ?? throw new ArgumentNullException();
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _service.GetProducts();
        return Ok(products);
    }

    [HttpGet("{productId}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> Get([FromRoute] int productId)
    {
        var product = await _service.GetProduct(productId);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("supplier/{supplierId:int}", Name = "GetProductsBySupplier")]
    public async Task<ActionResult<IEnumerable<SupplierProduct>>> GetProductsBySupplier(int supplierId)
    {
        var supplierProducts = await _service.GetProductsBySupplier(supplierId);
        if (supplierProducts == null)
        {
            return NotFound();
        }
        return Ok(supplierProducts);
    }



    // [HttpPut("{id:int}", Name = "UpdateProduct")]
    // public async Task<ActionResult<ProductUpdateRequest>> Put(int id, [FromBody] ProductUpdateRequest productUpdateRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", productUpdateRequest);

    //     productUpdateRequest.ProductId = id;

    //     var validator = await _validator.IsValidUpdate(productUpdateRequest);
    //     if (!validator.RequestValid)
    //     {
    //         _logger.LogWarning("Invalid Request");
    //         return BadRequest();
    //     }
    //     else
    //     {
    //         if (!validator.EntityFound)
    //         {
    //             _logger.LogWarning("Entity Not Found");
    //             return NotFound();
    //         }
    //     }

    //     await _service.UpdateProduct(productUpdateRequest);
    //     return Ok();
    // }

    // [HttpPut(Name = "DisableProduct"), Route("{id}/disable")]
    // public async Task<ActionResult<ProductDisableRequest>> Put(int id, [FromBody] ProductDisableRequest productDisableRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", productDisableRequest);

    //     productDisableRequest.ProductId = id;
    //     var validator = await _validator.IsValidDisable(productDisableRequest);
    //     if (!validator.RequestValid)
    //     {
    //         _logger.LogWarning("Invalid Request");
    //         return BadRequest();
    //     }
    //     else
    //     {
    //         if (!validator.EntityFound)
    //         {
    //             _logger.LogWarning("Entity Not Found");
    //             return NotFound();
    //         }
    //     }

    //     await _service.DisableProduct(productDisableRequest);
    //     return Ok();
    // }

    // [HttpPost(Name = "AddSupplierProduct")]
    // public async Task<ActionResult<SupplierProduct>> Post([FromBody] ProductAddRequest productAddRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", productAddRequest);

    //     if (!_validator.IsValidAdd(productAddRequest))
    //     {
    //         _logger.LogWarning("Invalid Request");
    //         return BadRequest();
    //     }

    //     var product = await _service.AddProduct(productAddRequest);
    //     return Ok(product);
    // }
}
