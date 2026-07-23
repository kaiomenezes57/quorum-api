using Quorum.Domain.Common;

namespace Quorum.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; } = Guid.CreateVersion7();
    
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
    private readonly List<IDomainEvent> _domainEvents = [];
    
    protected void AddDomainEvent(IDomainEvent domainEvent) 
        => _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents()
        => _domainEvents.Clear();
}