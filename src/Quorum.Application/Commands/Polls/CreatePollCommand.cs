using MediatR;

namespace Quorum.Application.Commands.Polls
{
    public record CreatePollCommand(
        string Name,
        string Description,
        Guid OwnerId,
        int VotesTarget) : IRequest<Guid>;
}
