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

    private Poll() { }
    
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

    public bool UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        
        Name = name;
        UpdateLastUpdatedAt();
        
        return true;
    }

    public bool UpdateDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
            return false;
        
        Description = description;
        UpdateLastUpdatedAt();
        
        return true;
    }

    public void AddOption(string name)
    {
        var option = new Option(name, Id);
        _options.Add(option);
        
        UpdateLastUpdatedAt();
    }

    public bool RemoveOption(Guid optionId)
    {
        var option = _options.Find(o => o.Id == optionId);
        if (option is null) 
            return false;

        if (!_options.Remove(option)) 
            return false;
        
        UpdateLastUpdatedAt();
        return true;
    }

    public bool AddVote(Guid optionId, Guid userId)
    {
        var option = _options.Find(o => o.Id == optionId);
        if (option is null) 
            return false;

        if (!option.AddVote(userId))
            return false; 
        
        UpdateLastUpdatedAt();
        return true;
    }

    public bool RemoveVote(Guid optionId, Guid userId)
    {
        var option = _options.Find(o => o.Id == optionId);
        if (option is null) 
            return false;

        if (!option.RemoveVote(userId))
            return false;

        UpdateLastUpdatedAt();
        return true;
    }
    
    private void UpdateLastUpdatedAt()
    {
        LastUpdatedAt = DateTime.UtcNow;
    }
}