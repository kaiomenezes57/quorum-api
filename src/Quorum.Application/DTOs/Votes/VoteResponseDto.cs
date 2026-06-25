namespace Quorum.Application.DTOs.Votes
{
    public record VoteResponseDto(
        Guid UserId,
        Guid PollId,
        Guid OptionId,
        DateTime VoteDate);
}
