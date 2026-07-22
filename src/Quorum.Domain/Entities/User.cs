using Quorum.Domain.ValueObjects;

namespace Quorum.Domain.Entities;

public class User : BaseEntity 
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Password Password { get; }

    public IReadOnlyList<Poll> Polls => _polls.AsReadOnly();
    private readonly List<Poll> _polls = [];
    
    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
    private readonly List<Vote> _votes = [];
    
    public IReadOnlyList<Prediction> Predictions => _predictions.AsReadOnly();
    private readonly List<Prediction> _predictions = [];

    private User() { }
    
    public User(string name, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);
        
        Name = name;
        Email = email;
        Password = Password.Create(passwordHash);
    }

    public bool VerifyPassword(string plainTextPassword) 
        => Password.Verify(plainTextPassword);
}