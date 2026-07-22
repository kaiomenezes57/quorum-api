using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Options.CreateOption;

public class CreateOptionCommandHandler(IPollRepository repository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateOptionCommand, WebResponse<string>>
{
    public async Task<WebResponse<string>> Handle(
        CreateOptionCommand request,
        CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);
        if (poll is null)
            return WebResponse<string>
                .Failure("Poll does not exist.")!;

        if (poll.UserId != request.UserId)
            return WebResponse<string>
                .Failure("Poll does not belong to this user.")!;
        
        if (!poll.AddOption(request.Name))
            return WebResponse<string>
                .Failure("Could not add option.")!;
        
        await unitOfWork.CommitAsync();

        return WebResponse<string>
            .Success(string.Empty)!;
    }
}