using Quorum.Domain.Enums;

namespace Quorum.Domain.Entities
{
    public class Poll
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public Guid OwnerId { get; private set; }
        public User Owner { get; private set; } = null!;

        public PollStatus Status { get; private set; }
        public int VotesTarget { get; private set; }

        public DateTime CreateDate { get; private set; }
        public DateTime LastModifierDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public IReadOnlyList<Option> Options => _options.AsReadOnly();
        private readonly List<Option> _options = [];

        public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();
        private readonly List<Vote> _votes = [];

        private Poll() { }

        public Poll(string name, string description, Guid ownerId, int votesTarget)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            ArgumentOutOfRangeException.ThrowIfLessThan(votesTarget, 3);

            Id = Guid.CreateVersion7();

            Name = name;
            Description = description;

            OwnerId = ownerId;

            Status = PollStatus.Open;
            VotesTarget = votesTarget;

            CreateDate = DateTime.UtcNow;
            LastModifierDate = DateTime.UtcNow;
        }

        public Option AddOption(string optionName)
        {
            if (_options.Any(x => x.Name == optionName))
                throw new Exception("Given option name already exists.");

            var option = new Option(
                optionName,
                Id);

            _options.Add(option);

            return option;
        }

        public Vote AddVote(Guid voterId, Guid optionId)
        {
            if (Status == PollStatus.Closed)
                throw new Exception("Não é possível votar em uma enquete encerrada.");

            if (_votes.Any(x => x.VoterId == voterId))
                throw new Exception("Este usuário já votou nesta enquete.");

            if (!_options.Any(x => x.Id == optionId))
                throw new Exception("A opção selecionada não pertence a esta enquete.");

            var vote = new Vote(
                voterId: voterId,
                pollId: Id,
                optionId: optionId);

            _votes.Add(vote);

            LastModifierDate = DateTime.UtcNow;

            if (_votes.Count >= VotesTarget)
                EndPoll();

            return vote;
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