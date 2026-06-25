using MediatR;

namespace Quorum.Application.Commands.User
{
    public record CreateUserCommand(
        string Username,
        string Email) : IRequest<Guid>;
}
