using MediatR;
using Quorum.Application.DTOs.Polls;

namespace Quorum.Application.Queries.Polls
{
    public record GetPollByIdQuery(
        Guid Id) : IRequest<PollResponseDto>;
}
