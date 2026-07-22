using Quorum.Application.Shared.Options;

namespace Quorum.Application.Shared.Polls;

public record PollResponseDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    int VoteGoal,
    int VoteCount,
    DateTime CreatedAt,
    DateTime LastUpdatedAt,
    IReadOnlyList<OptionResponseDto> Options);