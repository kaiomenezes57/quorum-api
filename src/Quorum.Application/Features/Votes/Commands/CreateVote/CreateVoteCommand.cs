using MediatR;

namespace Quorum.Application.Features.Votes.Commands.CreateVote
{
    public record CreateVoteCommand(
        Guid VoterId, 
        Guid PollId, 
        Guid OptionId) : IRequest<Guid>;
}
