using MediatR;

namespace Quorum.Application.Commands.Options
{
    public record CreateOptionCommand(
        string Name,
        Guid PollId) : IRequest<Guid>;
}
