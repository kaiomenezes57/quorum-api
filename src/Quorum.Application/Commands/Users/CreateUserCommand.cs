using MediatR;

namespace Quorum.Application.Commands.Users
{
    public record CreateUserCommand(
        string Username,
        string Email) : IRequest<Guid>;
}
