using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.GetPollById;

public class GetPollByIdQueryHandler(IPollRepository repository) :
    IRequestHandler<GetPollByIdQuery, DefaultResponse<PollResponseDto?>>
{
    public async Task<DefaultResponse<PollResponseDto?>> Handle(
        GetPollByIdQuery request,
        CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);

        return poll is null ? 
            DefaultResponse<PollResponseDto?>.Failure("Poll could not be found.") : 
            DefaultResponse<PollResponseDto?>.Success(poll.ToDto());
    }
}