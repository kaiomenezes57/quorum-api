using MediatR;
using Quorum.Application.Interfaces;
using Quorum.Application.Shared.Responses;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Predictions.CreatePrediction;

public class CreatePredictionCommandHandler(IPollRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePredictionCommand, WebResponse<string>>
{
    public async Task<WebResponse<string>> Handle(
        CreatePredictionCommand request, CancellationToken cancellationToken)
    {
        var poll = await repository.GetByIdAsync(request.PollId);
        if (poll is null)
            return WebResponse<string>
                .Failure("Poll does not exist.")!;

        if (!poll.AddPrediction(request.OptionId, request.UserId))
            return WebResponse<string>
                .Failure("Could not add prediction.")!;

        await unitOfWork.CommitAsync();
        
        return WebResponse<string>
            .Success(string.Empty)!;
    }
}