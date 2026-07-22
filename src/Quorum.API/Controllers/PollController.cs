using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Options.CreateOption;
using Quorum.Application.Features.Polls.CreatePoll;
using Quorum.Application.Features.Polls.GetAllPolls;
using Quorum.Application.Features.Polls.GetPollById;
using Quorum.Application.Features.Predictions.CreatePrediction;
using Quorum.Application.Features.Votes.CreateVote;

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
        var result = 
            await mediator.Send(new GetPollByIdQuery(pollId));
        
        return !result.IsSuccess ?
            NotFound(result.ErrorMessage) :
            Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess ?
            BadRequest(result.ErrorMessage) :
            Ok(result.Data);
    }

    [Authorize]
    [HttpPost("{pollId:guid}/options")]
    public async Task<IActionResult> CreateOption(
        Guid pollId, 
        [FromBody] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        
        var result = 
            await mediator.Send(new CreateOptionCommand(name, pollId, Guid.Parse(userId)));

        return !result.IsSuccess ? 
            BadRequest(result.ErrorMessage) : 
            Created();
    }

    [Authorize]
    [HttpPost("{pollId:guid}/options/{optionId:guid}/vote")]
    public async Task<IActionResult> Vote(Guid pollId, Guid optionId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result =
            await mediator.Send(new CreateVoteCommand(pollId, optionId, Guid.Parse(userId)));
        
        return !result.IsSuccess ? 
            BadRequest(result.ErrorMessage) : 
            Ok("Voted.");
    }
    
    [Authorize]
    [HttpPost("{pollId:guid}/options/{optionId:guid}/predict")]
    public async Task<IActionResult> Predict(Guid pollId, Guid optionId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result =
            await mediator.Send(new CreatePredictionCommand(pollId, optionId, Guid.Parse(userId)));
        
        return !result.IsSuccess ? 
            BadRequest(result.ErrorMessage) : 
            Ok("Predicted.");
    }
}