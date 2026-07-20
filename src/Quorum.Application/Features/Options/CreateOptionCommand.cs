using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Options;

public record CreateOptionCommand(string Name, Guid PollId) : 
    IRequest<DefaultResponse<string>>;