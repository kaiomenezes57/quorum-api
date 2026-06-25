using Quorum.Application.DTOs.Option;
using Quorum.Application.DTOs.Vote;
using Quorum.Domain.Entities;

namespace Quorum.Application.DTOs.Poll
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
