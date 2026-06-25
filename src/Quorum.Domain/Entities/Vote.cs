namespace Quorum.Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; private set; }

        public Guid VoterId { get; private set; }
        public User Voter { get; private set; }

        public Guid PollId { get; private set; }
        public Poll Poll { get; private set; }

        public Guid OptionId { get; private set; }
        public Option Option { get; private set; }

        public DateTime VoteDate { get; private set; }

        private Vote() { }

        public Vote(User voter, Poll poll, Option option)
        {
            Id = Guid.CreateVersion7();

            VoterId = voter.Id;
            Voter = voter;
            
            PollId = poll.Id;
            Poll = poll;

            Option = option;

            VoteDate = DateTime.UtcNow;
        }
    }
}
