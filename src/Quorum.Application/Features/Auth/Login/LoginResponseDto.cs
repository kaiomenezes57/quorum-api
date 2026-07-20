namespace Quorum.Application.Features.Auth.Login;

public record LoginResponseDto(Guid UserId, string Token);