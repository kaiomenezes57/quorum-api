namespace Quorum.Domain.Entities;

public class Poll : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime LastUpdatedAt { get; private set; }

    public IReadOnlyList<Option> Options => _options.AsReadOnly();
    private readonly List<Option> _options = [];
    
    public Guid UserId { get; }
    public User User { get; } = null!;

    private Poll()
    {
    }
    
    public Poll(string name, string description, Guid userId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(description);
        
        Name = name;
        Description = description;
        UserId = userId;
        
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void AddOption(string name)
    {
        var option = new Option(name, Id);
        _options.Add(option);
        
        LastUpdatedAt = DateTime.UtcNow;
    }
}