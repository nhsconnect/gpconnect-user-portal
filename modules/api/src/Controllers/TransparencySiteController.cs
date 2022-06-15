using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

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
    public async Task<ActionResult> Get([FromQuery] TransparencySiteRequest query)
    {
        _logger.LogInformation("Received Request {@query}", query);

        if (!_validator.IsValidRequest(query))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        var sites = await _service.GetMatchingSitesAsync(query);

        return Ok(sites);
    }

    [HttpGet("{id}", Name = "GetTransparencySite")]
    public async Task<ActionResult> GetTransparencySite([FromRoute] string id)
    {
        _logger.LogInformation("Received Request {@id}", id);

        if (!_validator.IsValidId(id))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        var site = await _service.GetSiteAsync(Guid.Parse(id));

        if (site == null)
        {
            return NotFound();
        }

        return Ok(site);
    }
}