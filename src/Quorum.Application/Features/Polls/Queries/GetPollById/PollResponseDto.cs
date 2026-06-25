using Quorum.Application.Features.Options.Queries;
using Quorum.Application.Features.Votes.Queries;
using Quorum.Domain.Entities;

namespace Quorum.Application.Features.Polls.Queries.GetPollById
{
    public record PollResponseDto(
        string Name, 
        string Description,
        Guid OwnerId,
        int Status,
        int VotesTarget,
        DateTime CreateDate,
        DateTime LastModifierDate,
        DateTime EndDate,
        IReadOnlyList<OptionResponseDto> Options,
        IReadOnlyList<VoteResponseDto> Votes);
}
