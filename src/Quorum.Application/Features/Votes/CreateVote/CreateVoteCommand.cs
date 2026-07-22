using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Votes.CreateVote;

public record CreateVoteCommand(Guid PollId, Guid OptionId, Guid UserId) 
    : IRequest<WebResponse<string>>;