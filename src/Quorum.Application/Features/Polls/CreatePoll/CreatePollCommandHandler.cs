using MediatR;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.CreatePoll;

public class CreatePollCommandHandler(IPollRepository repository) : 
    IRequestHandler<CreatePollCommand, DefaultResponse<Guid>>
{
    public async Task<DefaultResponse<Guid>> Handle(
        CreatePollCommand request,
        CancellationToken cancellationToken)
    {
        var poll = new Poll(
            request.Name,
            request.Description,
            request.UserId);

        await repository.CreateAsync(poll);
        return DefaultResponse<Guid>.Success(poll.Id);
    }
}