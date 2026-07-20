using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public record GetAllPollsQuery : 
    IRequest<DefaultResponse<IReadOnlyList<PollResponseDto>>>;