namespace Quorum.Domain.Entities;

public class Prediction : BaseEntity
{
    public Guid UserId { get; }
    public User User { get; } = null!;
    
    public Guid PollId { get; }
    public Poll Poll { get; } = null!;
    
    public Guid OptionId { get; }
    public Option Option { get; } = null!;
 
    public Prediction(Guid userId, Guid pollId, Guid optionId)
    {
        UserId = userId;
        PollId = pollId;
        OptionId = optionId;
    }
}