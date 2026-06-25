using MediatR;

namespace Quorum.Application.Features.Polls.Commands.CreatePoll
{
    public record CreatePollCommand(
        string Name,
        string Description,
        Guid OwnerId,
        int VotesTarget) : IRequest<Guid>;
}
