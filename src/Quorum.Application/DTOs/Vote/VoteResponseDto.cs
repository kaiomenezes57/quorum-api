namespace Quorum.Application.DTOs.Vote
{
    public record VoteResponseDto(
        Guid UserId,
        Guid PollId,
        Guid OptionId,
        DateTime VoteDate);
}
