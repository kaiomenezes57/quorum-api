using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Polls.GetAllPolls;

namespace Quorum.API.Controllers;

[ApiController]
[Route("api/polls")]
public class PollController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllPollsQuery());

        return result.Count == 0 ? 
            NotFound() : 
            Ok(result);
    }
}