using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Auth.Register;

public record RegisterCommand(string Name, string Email, string Password)
    : IRequest<DefaultResponse<Guid>>;