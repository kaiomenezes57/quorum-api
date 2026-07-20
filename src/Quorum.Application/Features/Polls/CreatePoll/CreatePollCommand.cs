using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Polls.CreatePoll;

public record CreatePollCommand(
    string Name, 
    string Description, 
    Guid UserId) : IRequest<DefaultResponse<Guid>>;