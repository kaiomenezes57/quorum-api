using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Options;
using Quorum.Application.Features.Polls.CreatePoll;
using Quorum.Application.Features.Polls.GetAllPolls;
using Quorum.Application.Features.Polls.GetPollById;

namespace Quorum.API.Controllers;

[ApiController]
[Route("api/polls")]
public class PollController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllPollsQuery());

        return !result.IsSuccess ? 
            NotFound(result.ErrorMessage) : 
            Ok(result.Data);
    }

    [HttpGet("{pollId:guid}")]
    public async Task<IActionResult> GetById(Guid pollId)
    {
        var query = new GetPollByIdQuery(pollId);
        var result = await mediator.Send(query);
        
        return !result.IsSuccess ?
            NotFound(result.ErrorMessage) :
            Ok(result.Data);
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePollCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess ?
            BadRequest(result.ErrorMessage) :
            Ok(result.Data);
    }

    [HttpPost("{pollId:guid}/options")]
    //[Authorize]
    public async Task<IActionResult> Vote(
        Guid pollId, 
        [FromBody] string name)
    {
        var result = 
            await mediator.Send(new CreateOptionCommand(name, pollId));

        return !result.IsSuccess ? 
            BadRequest(result.ErrorMessage) : 
            Created();
    }
}