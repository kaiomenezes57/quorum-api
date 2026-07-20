namespace Quorum.Domain.Entities;

public class Option : BaseEntity
{
    public string Name { get; }
    
    public Guid PollId { get; }
    public Poll Poll { get; } = null!;
    
    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
    private readonly List<Vote> _votes = [];

    private Option() { }

    public Option(string name, Guid pollId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Name = name;
        PollId = pollId;
    }

    public bool AddVote(Guid userId)
    {
        if (_votes.Any(v => v.UserId == userId))
            return false;

        var vote = new Vote(userId, Id);
        _votes.Add(vote);

        return true;
    }

    public bool RemoveVote(Guid userId)
    {
        if (_votes.All(v => v.UserId != userId))
            return false;
        
        _votes.RemoveAll(v => v.UserId == userId);
        return true;
    }
}