using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Commands.Options;

namespace Quorum.Api.Controllers
{
    [ApiController]
    [Route("api/v1/options")]
    public sealed class OptionController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateOptionCommand command)
        {
            var id = await mediator.Send(command);
            if (id.Equals(default))
                return BadRequest();

            return Ok(id);
        }
    }
}
