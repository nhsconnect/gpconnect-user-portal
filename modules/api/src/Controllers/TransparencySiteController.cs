using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace gpconnect_user_portal.api.Controllers;

[ApiController]
[Route("transparency-site")]
public class TransparencySiteController : ControllerBase
{
    private readonly ILogger<TransparencySiteController> _logger;
    private readonly ITransparencySiteRequestValidator _validator;
    private readonly ITransparencySiteService _service;

    public TransparencySiteController(ITransparencySiteRequestValidator validator, ITransparencySiteService service, ILogger<TransparencySiteController> logger)
    {
        _logger = logger;
        _validator = validator;
        _service = service;
    }

    [HttpGet(Name = "GetMatchingSites")]
    public ActionResult<IEnumerable<TransparencySite>> Get([FromQuery] TransparencySiteRequest query)
    {
        _logger.LogInformation("Received Request {@query}", query);

        if (!_validator.IsValid(query))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        var sites = _service.GetMatchingSites(query);
        
        return Ok(sites);
    }
}
