using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Auth.Login;
using Quorum.Application.Features.Auth.Register;

namespace Quorum.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess ?
            BadRequest(result.ErrorMessage) :
            Ok(result.Data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess ?
            BadRequest(result.ErrorMessage) :
            Created();
    }
}