using Quorum.Application.DTOs.Options;
using Quorum.Application.DTOs.Votes;
using Quorum.Domain.Entities;

namespace Quorum.Application.DTOs.Polls
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
