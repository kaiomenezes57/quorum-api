using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Options;

public class CreateOptionCommandHandler(IPollRepository repository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateOptionCommand, DefaultResponse<string>>
{
    public async Task<DefaultResponse<string>> Handle(
        CreateOptionCommand request,
        CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);
        if (poll is null)
            return DefaultResponse<string>
                .Failure("Poll does not exist.")!;
        
        if (!poll.AddOption(request.Name))
            return DefaultResponse<string>
                .Failure("Could not add option.")!;
        
        await unitOfWork.CommitAsync();

        return DefaultResponse<string>
            .Success(string.Empty)!;
    }
}