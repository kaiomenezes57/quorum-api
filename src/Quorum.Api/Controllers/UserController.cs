using MediatR;
using Microsoft.AspNetCore.Mvc;
using Quorum.Application.Commands.User;
using Quorum.Application.Queries.Users;

namespace Quorum.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public sealed class UserController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var id = await mediator.Send(command);
            if (id.Equals(default))
                return BadRequest();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
