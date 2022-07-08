using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

[ApiController]
[Route("agreement")]
public class AgreementController : ControllerBase
{
    private readonly IAgreementService _acceptanceService;
    private readonly ILogger<AgreementController> _logger;

    public AgreementController(IAgreementService acceptanceService, ILogger<AgreementController> logger)
    {
        _acceptanceService = acceptanceService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAgreementEntity([FromBody] AgreementInformation info)
    {
        await _acceptanceService.CreateAgreementAsync(info);
        
        return StatusCode(201);
    }
}