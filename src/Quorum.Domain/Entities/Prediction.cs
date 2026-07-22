using Quorum.Domain.Enums;

namespace Quorum.Domain.Entities;

public class Prediction : BaseEntity
{
    public PredictionResult Result { get; private set; }
    
    public Guid UserId { get; }
    public User User { get; } = null!;
    
    public Guid PollId { get; }
    public Poll Poll { get; } = null!;
    
    public Guid OptionId { get; }
    public Option Option { get; } = null!;
     
    private Prediction() { }
    
    public Prediction(Guid userId, Guid pollId, Guid optionId)
    {
        Result = PredictionResult.None;
        
        UserId = userId;
        PollId = pollId;
        OptionId = optionId;
    }
    
    public void SetResult(PredictionResult result) 
        => Result = result; 
}