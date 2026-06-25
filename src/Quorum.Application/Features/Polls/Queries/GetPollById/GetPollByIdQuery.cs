using MediatR;

namespace Quorum.Application.Features.Polls.Queries.GetPollById
{
    public record GetPollByIdQuery(
        Guid Id) : IRequest<PollResponseDto>;
}
