using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

[ApiController]
[Route("ccg")]
public class CcgController : ControllerBase
{
    private readonly ILogger<CcgController> _logger;
    private readonly ICcgService _service;

    public CcgController(ICcgService service, ILogger<CcgController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _service = service ?? throw new ArgumentNullException();
    }

    [HttpGet(Name = "GetCcgs")]
    public async Task<ActionResult<IEnumerable<Ccg>>> Get()
    {
        var ccgs = await _service.GetCcgs();
        return Ok(ccgs);
    }
}
