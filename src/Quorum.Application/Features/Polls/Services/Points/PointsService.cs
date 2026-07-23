using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Features.Polls.Services.Points;

public class PointsService(
    IPollRepository pollRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IPointsService
{
    public async Task AwardAsync(Guid pollId, Guid mostVotedOptionId)
    {
        var poll = await pollRepository.GetByIdAsync(pollId);
        var mostVotedOption = poll?.Options.FirstOrDefault(o => o.Id == mostVotedOptionId);

        if (mostVotedOption is null)
            return;

        var userIds = mostVotedOption.Predictions 
            .Select(p => p.UserId);
        
        foreach (var userId in userIds)
        {
            var user = await userRepository.GetByIdAsync(userId);
            user?.AddPredictionPoints();
        }

        await unitOfWork.CommitAsync();
    }
}