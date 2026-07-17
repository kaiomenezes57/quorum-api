namespace Quorum.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; } = Guid.CreateVersion7();
}