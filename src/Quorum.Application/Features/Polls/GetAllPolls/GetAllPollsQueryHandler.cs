using MediatR;
using Quorum.Application.Shared.Polls;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.GetAllPolls;

public class GetAllPollsQueryHandler(IPollRepository repository) : 
    IRequestHandler<GetAllPollsQuery, 
    DefaultResponse<IReadOnlyList<PollResponseDto>>>
{
    public async Task<DefaultResponse<IReadOnlyList<PollResponseDto>>> Handle(
        GetAllPollsQuery request, 
        CancellationToken cancellationToken)
    {
        var polls = await repository.GetAllAsync();
        if (polls.Count == 0)
            return DefaultResponse<IReadOnlyList<PollResponseDto>>
                .Failure("No Polls found.")!;
        
        var pollsResponse = polls.Select(p => p.ToDto()).ToList();

        return DefaultResponse<IReadOnlyList<PollResponseDto>>
            .Success(pollsResponse.AsReadOnly())!;
    }
}