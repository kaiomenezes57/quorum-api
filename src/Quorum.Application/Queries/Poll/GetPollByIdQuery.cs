using MediatR;
using Quorum.Application.DTOs.Poll;

namespace Quorum.Application.Queries.Poll
{
    public record GetPollByIdQuery(
        Guid Id) : IRequest<PollResponseDto>;
}
