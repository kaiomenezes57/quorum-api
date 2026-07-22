using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.GetPollById;

public class GetPollByIdQueryHandler(IPollRepository repository) :
    IRequestHandler<GetPollByIdQuery, WebResponse<PollResponseDto?>>
{
    public async Task<WebResponse<PollResponseDto?>> Handle(
        GetPollByIdQuery request,
        CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);

        return poll is null ? 
            WebResponse<PollResponseDto?>.Failure("Poll could not be found.") : 
            WebResponse<PollResponseDto?>.Success(poll.ToDto());
    }
}