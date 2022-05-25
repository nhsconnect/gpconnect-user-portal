using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        _logger = logger;
        _validator = validator;
        _service = service;
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

        if(careSetting == null)
        {
            return NotFound();
        }
        return Ok(careSetting);
    }

    [HttpPut(Name = "UpdateCareSetting"), Route("update")]
    public async Task<ActionResult> Put([FromBody] CareSettingUpdateRequest careSettingUpdateRequest)
    {
        _logger.LogInformation("Received Request {@query}", careSettingUpdateRequest);

        if (!_validator.IsValidUpdate(careSettingUpdateRequest))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        await _service.UpdateCareSetting(careSettingUpdateRequest);
        return Ok();
    }

    [HttpPut(Name = "EnableDisableCareSetting"), Route("enable-disable")]
    public async Task<ActionResult> Put([FromBody] CareSettingEnableDisableRequest careSettingEnableDisableRequest)
    {
        _logger.LogInformation("Received Request {@query}", careSettingEnableDisableRequest);

        if (!_validator.IsValidEnableDisable(careSettingEnableDisableRequest))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        await _service.EnableDisableCareSetting(careSettingEnableDisableRequest);
        return Ok();
    }

    [HttpPost(Name = "AddCareSetting"), Route("add")]
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
