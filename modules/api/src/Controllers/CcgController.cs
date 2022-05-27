using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetCcgs")]
    public async Task<ActionResult<IEnumerable<Ccg>>> Get()
    {
        var ccgs = await _service.GetCcgs();
        return Ok(ccgs);
    }
}
