using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public class GetAllPollsQueryHandler(IPollRepository repository) : 
    IRequestHandler<GetAllPollsQuery,
    IReadOnlyList<PollResponseDto>>
{
    public async Task<IReadOnlyList<PollResponseDto>> Handle(
        GetAllPollsQuery request, 
        CancellationToken cancellationToken)
    {
        var polls = await repository.GetAllAsync();
        if (polls.Count == 0)
            return [];
        
        var pollsResponse = polls.Select(p => p.ToDto()).ToList();

        return pollsResponse.AsReadOnly();
    }
}