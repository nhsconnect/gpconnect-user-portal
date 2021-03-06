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
[Route("care-setting")]
public class CareSettingController : ControllerBase
{
    private readonly ILogger<CareSettingController> _logger;
    private readonly ICareSettingRequestValidator _validator;
    private readonly ICareSettingService _service;

    public CareSettingController(ICareSettingRequestValidator validator, ICareSettingService service, ILogger<CareSettingController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _validator = validator ?? throw new ArgumentNullException();
        _service = service ?? throw new ArgumentNullException();
    }

    [HttpGet(Name = "GetCareSettings")]
    public async Task<ActionResult<IEnumerable<CareSetting>>> Get()
    {
        var careSettings = await _service.GetCareSettings();
        return Ok(careSettings);
    }

    [HttpGet("{careSettingId}", Name = "GetCareSetting")]
    public async Task<ActionResult<CareSetting>> Get([FromRoute] int careSettingId)
    {
        var careSetting = await _service.GetCareSetting(careSettingId);
        if (careSetting == null)
        {
            return NotFound();
        }
        return Ok(careSetting);
    }

    [HttpPut("{id:int}", Name = "UpdateCareSetting")]
    public async Task<ActionResult<CareSettingUpdateRequest>> Put(int id, [FromBody] CareSettingUpdateRequest careSettingUpdateRequest)
    {
        _logger.LogInformation("Received Request {@query}", careSettingUpdateRequest);       

        careSettingUpdateRequest.CareSettingId = id;
        var validator = await _validator.IsValidUpdate(careSettingUpdateRequest);
        if (!validator.RequestValid)
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }
        else
        {
            if (!validator.EntityFound)
            {
                _logger.LogWarning("Entity Not Found");
                return NotFound();
            }
        }

        await _service.UpdateCareSetting(careSettingUpdateRequest);
        return Ok();
    }

    [HttpPut(Name = "DisableCareSetting"), Route("{id}/disable")]
    public async Task<ActionResult<CareSettingDisableRequest>> Put(int id, [FromBody] CareSettingDisableRequest careSettingDisableRequest)
    {
        _logger.LogInformation("Received Request {@query}", careSettingDisableRequest);

        careSettingDisableRequest.CareSettingId = id;
        var validator = await _validator.IsValidDisable(careSettingDisableRequest);
        if (!validator.RequestValid)
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }
        else
        {
            if (!validator.EntityFound)
            {
                _logger.LogWarning("Entity Not Found");
                return NotFound();
            }
        }


        await _service.DisableCareSetting(careSettingDisableRequest);
        return Ok();
    }

    [HttpPost(Name = "AddCareSetting")]
    public async Task<ActionResult<CareSetting>> Post([FromBody] CareSettingAddRequest careSettingAddRequest)
    {
        _logger.LogInformation("Received Request {@query}", careSettingAddRequest);

        if (!_validator.IsValidAdd(careSettingAddRequest))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        var careSetting = await _service.AddCareSetting(careSettingAddRequest);
        return Ok(careSetting);
    }
}
