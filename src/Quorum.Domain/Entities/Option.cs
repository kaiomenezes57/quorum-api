namespace Quorum.Domain.Entities;

public class Option : BaseEntity
{
    public string Name { get; }
    
    public Guid PollId { get; }
    public Poll Poll { get; } = null!;
    
    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
    private readonly List<Vote> _votes = [];

    private Option()
    {
    }

    public Option(string name, Guid pollId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Name = name;
        PollId = pollId;
    }

    public void AddVote(Guid userId)
    {
        if (_votes.Any(v => v.UserId == userId))
            return;

        var vote = new Vote(userId, Id);
        _votes.Add(vote);
    }
}