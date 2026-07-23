namespace Quorum.Application.Features.Polls.Services.Points;

public interface IPointsService
{
    Task AwardAsync(Guid pollId, Guid mostVotedOptionId);
}