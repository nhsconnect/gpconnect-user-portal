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
[Route("supplier")]
public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;
    private readonly ISupplierRequestValidator _validator;
    private readonly ISupplierService _service;

    public SupplierController(ISupplierRequestValidator validator, ISupplierService service, ILogger<SupplierController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _validator = validator ?? throw new ArgumentNullException();
        _service = service ?? throw new ArgumentNullException();
    }

    [HttpGet(Name = "GetSuppliers")]
    public async Task<ActionResult<IEnumerable<Supplier>>> Get()
    {
        var suppliers = await _service.GetSuppliers();
        return Ok(suppliers);
    }

    [HttpGet("{supplierId}", Name = "GetSupplier")]
    public async Task<ActionResult<Supplier>> Get([FromRoute] int supplierId)
    {
        var supplier = await _service.GetSupplier(supplierId);
        if (supplier == null)
        {
            return NotFound();
        }
        return Ok(supplier);
    }

    // [HttpPut("{id:int}", Name = "UpdateSupplier")]
    // public async Task<ActionResult<SupplierUpdateRequest>> Put(int id, [FromBody] SupplierUpdateRequest supplierUpdateRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", supplierUpdateRequest);

    //     supplierUpdateRequest.SupplierId = id;

    //     var validator = await _validator.IsValidUpdate(supplierUpdateRequest);
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

    //     await _service.UpdateSupplier(supplierUpdateRequest);
    //     return Ok();
    // }

    // [HttpPut(Name = "DisableSupplier"), Route("{id}/disable")]
    // public async Task<ActionResult<SupplierDisableRequest>> Put(int id, [FromBody] SupplierDisableRequest supplierDisableRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", supplierDisableRequest);

    //     supplierDisableRequest.SupplierId = id;
    //     var validator = await _validator.IsValidDisable(supplierDisableRequest);
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

    //     await _service.DisableSupplier(supplierDisableRequest);
    //     return Ok();
    // }

    // [HttpPost(Name = "AddSupplier")]
    // public async Task<ActionResult<Supplier>> Post([FromBody] SupplierAddRequest supplierAddRequest)
    // {
    //     _logger.LogInformation("Received Request {@query}", supplierAddRequest);

    //     if (!_validator.IsValidAdd(supplierAddRequest))
    //     {
    //         _logger.LogWarning("Invalid Request");
    //         return BadRequest();
    //     }

    //     var supplier = await _service.AddSupplier(supplierAddRequest);
    //     return Ok(supplier);
    // }
}
