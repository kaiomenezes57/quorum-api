using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Auth.Login;

public record LoginCommand(string Email, string Password)
    : IRequest<DefaultResponse<LoginResponseDto>>;