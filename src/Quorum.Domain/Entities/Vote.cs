namespace Quorum.Domain.Entities;

public class Vote : BaseEntity
{
    public DateTime VotedAt { get; }
    
    public Guid UserId { get; }
    public User User { get; } = null!;
    
    public Guid OptionId { get; }
    public Option Option { get; } = null!;

    private Vote() { }
    
    public Vote(Guid userId, Guid optionId)
    {
        VotedAt = DateTime.UtcNow;
        
        UserId = userId;
        OptionId = optionId;
    }
}