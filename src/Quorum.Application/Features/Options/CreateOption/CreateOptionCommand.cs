using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Options.CreateOption;

public record CreateOptionCommand(string Name, Guid PollId, Guid UserId) : 
    IRequest<WebResponse<string>>;