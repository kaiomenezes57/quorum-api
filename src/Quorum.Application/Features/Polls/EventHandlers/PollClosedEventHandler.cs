using MediatR;
using Quorum.Application.Features.Polls.Services.Points;
using Quorum.Domain.DomainEvents.Polls;

namespace Quorum.Application.Features.Polls.EventHandlers;

public class PollClosedEventHandler(IPointsService pointsService) 
    : INotificationHandler<PollClosedEvent>
{
    public async Task Handle(PollClosedEvent notification, 
        CancellationToken cancellationToken)
    {
        await pointsService.AwardAsync(
            notification.PollId,
            notification.MostVotedOptionId);
    }
}