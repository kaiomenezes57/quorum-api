using Quorum.Domain.Enums;

namespace Quorum.Domain.Entities
{
    public class Poll
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public Guid OwnerId { get; private set; }
        public User Owner { get; private set; }

        public PollStatus Status { get; private set; }
        public int VotesTarget { get; private set; }

        public DateTime CreateDate { get; private set; }
        public DateTime LastModifierDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public IReadOnlyList<Option> Options => _options.AsReadOnly();
        private readonly List<Option> _options;

        public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
        private readonly List<Vote> _votes = [];

        private Poll() { }

        public Poll(string name, string description, User owner, int votesTarget, List<Option> options)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            ArgumentNullException.ThrowIfNull(owner);

            ArgumentOutOfRangeException.ThrowIfLessThan(votesTarget, 3);

            ArgumentNullException.ThrowIfNull(options);
            ArgumentOutOfRangeException.ThrowIfLessThan(options.Count, 2);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(options.Count, 10);

            Id = Guid.CreateVersion7();

            Name = name;
            Description = description;

            OwnerId = owner.Id;
            Owner = owner;

            Status = PollStatus.Open;
            VotesTarget = votesTarget;

            _options = options;

            CreateDate = DateTime.UtcNow;
            LastModifierDate = DateTime.UtcNow;
        }

        public void AddVote(Vote vote)
        {
            if (Status == PollStatus.Closed)
                return;

            if (_votes.Any(x => x.VoterId == vote.VoterId))
                return;

            _votes.Add(vote);

            LastModifierDate = DateTime.UtcNow;

            if (_votes.Count >= VotesTarget)
                EndPoll();
        }

        public bool EndPoll()
        {
            if (_votes.Count < VotesTarget)
                return false;

            LastModifierDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;

            Status = PollStatus.Closed;

            return true;
        }
    }
}