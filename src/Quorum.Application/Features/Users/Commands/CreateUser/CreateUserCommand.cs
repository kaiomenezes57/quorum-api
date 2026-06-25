using MediatR;

namespace Quorum.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string Username,
        string Email) : IRequest<Guid>;
}
