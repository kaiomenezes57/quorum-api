namespace Quorum.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public IReadOnlyList<Poll> Polls => _polls.AsReadOnly();
        private readonly List<Poll> _polls = [];

        private User() { }

        public User(string username, string email)
        {
            Id = Guid.CreateVersion7();
            Username = username;
            Email = email;
        }

        public void AddPoll(Poll poll) 
            => _polls.Add(poll);
    }
}
