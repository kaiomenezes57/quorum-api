using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Votes.CreateVote;

public class CreateVoteCommandHandler(IPollRepository repository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateVoteCommand, WebResponse<string>>
{
    public async Task<WebResponse<string>> Handle(
        CreateVoteCommand request, CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);
        if (poll is null)
            return WebResponse<string>
                .Failure("Poll does not exist.")!;

        if (!poll.AddVote(request.OptionId, request.UserId))
            return WebResponse<string>
                .Failure("Could not add vote.")!;

        await unitOfWork.CommitAsync();
        
        return WebResponse<string>
            .Success(string.Empty)!;
    }
}