using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _service;

    public UserController(IUserService service, ILogger<UserController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException();
        _service = service ?? throw new ArgumentNullException();
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var users = await _service.GetUsers();
        return Ok(users);
    }
}
