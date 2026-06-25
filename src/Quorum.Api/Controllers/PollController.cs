using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Options.Commands.CreateOption;
using Quorum.Application.Features.Polls.Commands.CreatePoll;
using Quorum.Application.Features.Polls.Queries.GetPollById;

namespace Quorum.Api.Controllers
{
    [ApiController]
    [Route("api/polls")]
    public sealed class PollController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePollCommand command)
        {
            var id = await mediator.Send(command);
            if (id.Equals(default))
                return BadRequest();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetPollByIdQuery(id));
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("{pollId}/options")]
        public async Task<IActionResult> CreateOption(
            Guid pollId,
            [FromBody] string optionName)
        {
            var id = await mediator.Send(new CreateOptionCommand(optionName, pollId));
            if (id.Equals(default))
                return BadRequest();

            return Ok(id);
        }
    }
}
