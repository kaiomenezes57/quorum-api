namespace Quorum.Application.DTOs.Users
{
    public record UserResponseDto(
        string Username,
        string Email,
        IReadOnlyList<Guid> PollIds);
}
