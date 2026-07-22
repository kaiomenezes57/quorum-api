using MediatR;
using Quorum.Application.Shared.Responses;

namespace Quorum.Application.Features.Predictions.CreatePrediction;

public record CreatePredictionCommand(Guid PollId, Guid OptionId, Guid UserId) 
    : IRequest<WebResponse<string>>;