namespace Quorum.Application.Features.Votes.Queries
{
    public record VoteResponseDto(
        Guid UserId,
        Guid PollId,
        Guid OptionId,
        DateTime VoteDate);
}
