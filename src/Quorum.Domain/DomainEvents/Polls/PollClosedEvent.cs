using Quorum.Domain.Common;

namespace Quorum.Domain.DomainEvents.Polls;

public record PollClosedEvent(
    Guid PollId, 
    Guid MostVotedOptionId) : IDomainEvent;