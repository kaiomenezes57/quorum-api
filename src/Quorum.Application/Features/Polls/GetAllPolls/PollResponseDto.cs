using Quorum.Application.Shared.Options;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public record PollResponseDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime LastUpdatedAt,
    IReadOnlyList<OptionResponseDto> Options);