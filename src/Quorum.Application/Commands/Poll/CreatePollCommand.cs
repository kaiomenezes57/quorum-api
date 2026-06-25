using MediatR;

namespace Quorum.Application.Commands.Poll
{
    public record CreatePollCommand(
        string Name,
        string Description,
        Guid OwnerId,
        int VotesTarget) : IRequest<Guid>;
}
