using Quorum.Domain.Enums;

namespace Quorum.Domain.Entities;

public class Poll : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public int VoteGoal { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdatedAt { get; private set; }

    public IReadOnlyList<Option> Options => _options.AsReadOnly();
    private readonly List<Option> _options = [];
    
    public Guid UserId { get; }
    public User User { get; } = null!;

    private Poll() { }
    
    public Poll(string name, string description, Guid userId, int voteGoal)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(description);
        ArgumentOutOfRangeException.ThrowIfLessThan(voteGoal, 3);

        Name = name;
        Description = description;
        VoteGoal = voteGoal;
        UserId = userId;
        
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public bool UpdateInformation(string name, string description)
    {
        if (string.IsNullOrEmpty(name) || 
            string.IsNullOrEmpty(description))
            return false;
        
        Name = name;
        Description = description;
        
        RefreshLastUpdatedAt();
        return true;
    }

    public bool AddOption(string name)
    {
        if (_options.Any(o => o.Name == name))
            return false;
        
        var option = new Option(name, Id);
        _options.Add(option);
        
        RefreshLastUpdatedAt();
        return true;
    }

    public bool RemoveOption(Guid optionId)
    {
        if (_options.Find(o => o.Id == optionId) 
            is not { } option)
            return false;

        if (!_options.Remove(option)) 
            return false;
        
        RefreshLastUpdatedAt();
        return true;
    }

    public bool AddVote(Guid optionId, Guid userId)
    {
        if (HasAchievedVotesGoal())
            return false;

        if (_options.Find(o => o.Id == optionId) 
            is not { } option)
            return false;

        if (!option.AddVote(userId))
            return false;

        RefreshLastUpdatedAt();

        if (HasAchievedVotesGoal())
            SetPredictionsResults();

        return true;
    }

    public bool AddPrediction(Guid optionId, Guid userId)
    {
        if (_options.Find(o => o.Id == optionId) 
            is not { } option) 
            return false;

        if (!option.AddPrediction(userId))
            return false;
        
        RefreshLastUpdatedAt();
        return true;
    }

    private void SetPredictionsResults()
    {
        foreach (var prediction in _options.SelectMany(o => o.Predictions))
            prediction.SetResult(PredictionResult.Failed);

        if (_options.MaxBy(o => o.Votes.Count) 
            is not { } winningOption) 
            return;
        
        foreach (var prediction in winningOption.Predictions)
            prediction.SetResult(PredictionResult.Success);
    }

    private bool HasAchievedVotesGoal()
    {
        var totalVotes = _options.Sum(o => o.Votes.Count);
        return totalVotes >= VoteGoal;
    }
    
    private void RefreshLastUpdatedAt()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}