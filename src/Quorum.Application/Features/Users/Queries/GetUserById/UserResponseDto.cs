namespace Quorum.Application.Features.Users.Queries.GetUserById
{
    public record UserResponseDto(
        string Username,
        string Email,
        IReadOnlyList<Guid> PollIds);
}
