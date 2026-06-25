using MediatR;

namespace Quorum.Application.Features.Options.Commands.CreateOption
{
    public record CreateOptionCommand(
        string Name,
        Guid PollId) : IRequest<Guid>;
}
