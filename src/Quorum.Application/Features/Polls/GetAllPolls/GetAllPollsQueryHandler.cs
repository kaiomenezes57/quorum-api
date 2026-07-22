using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public class GetAllPollsQueryHandler(IPollRepository repository) : 
    IRequestHandler<GetAllPollsQuery, 
    WebResponse<IReadOnlyList<PollResponseDto>>>
{
    public async Task<WebResponse<IReadOnlyList<PollResponseDto>>> Handle(
        GetAllPollsQuery request, 
        CancellationToken cancellationToken)
    {
        var polls = await repository.GetAllAsync();
        if (polls.Count == 0)
            return WebResponse<IReadOnlyList<PollResponseDto>>
                .Failure("No Polls found.")!;
        
        var pollsResponse = polls.Select(p => p.ToDto()).ToList();

        return WebResponse<IReadOnlyList<PollResponseDto>>
            .Success(pollsResponse.AsReadOnly())!;
    }
}