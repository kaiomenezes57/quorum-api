using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Features.Polls.Commands.CreatePoll;
using Quorum.Application.Features.Polls.Queries.GetPollById;

namespace Quorum.Api.Controllers
{
    [ApiController]
    [Route("api/v1/polls")]
    public sealed class PollController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreatePollCommand command)
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
    }
}
