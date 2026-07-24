namespace Quorum.Domain.ValueObjects;

public class Password
{
    public string Hash { get; }

    private Password() { }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password Create(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Password cannot be null or whitespace.");

        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

        return new Password(passwordHash);
    }

    public bool Verify(string plainTextPassword) 
        => BCrypt.Net.BCrypt.Verify(plainTextPassword, Hash);
}