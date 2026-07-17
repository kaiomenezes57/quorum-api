using MediatR;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public record GetAllPollsQuery : IRequest<IReadOnlyList<PollResponseDto>>;