using Quorum.Domain.ValueObjects;

namespace Quorum.Domain.Entities;

public class User : BaseEntity 
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Password Password { get; private set; }

    public IReadOnlyList<Poll> Polls => _polls.AsReadOnly();
    private readonly List<Poll> _polls = [];

    private User()
    {
    }
    
    public User(string name, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);
        
        Name = name;
        Email = email;
        Password = Password.Create(passwordHash);
    }

    public void AddPoll(string name, string description)
    {
        var poll = new Poll(name, description, Id);
        _polls.Add(poll);
    }

    public bool VerifyPassword(string plainTextPassword)
    {
        return Password.Verify(plainTextPassword);
    }
}