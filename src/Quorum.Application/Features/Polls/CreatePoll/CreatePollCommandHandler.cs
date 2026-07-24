using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.CreatePoll;

public class CreatePollCommandHandler(IPollRepository repository, IUnitOfWork unitOfWork) : 
    IRequestHandler<CreatePollCommand, WebResponse<Guid>>
{
    public async Task<WebResponse<Guid>> Handle(
        CreatePollCommand request,
        CancellationToken cancellationToken)
    {
        var poll = new Poll(
            request.Name,
            request.Description,
            request.UserId,
            request.VoteGoal);

        await repository.CreateAsync(poll);
        
        await unitOfWork.CommitAsync();
        
        return WebResponse<Guid>.Success(poll.Id);
    }
}