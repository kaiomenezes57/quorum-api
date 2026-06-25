namespace Quorum.Domain.Entities
{
    public class Option
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public Guid PollId { get; private set; }
        public Poll Poll { get; private set; } = null!;

        private Option() { }

        public Option(string name, Guid pollId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Id = Guid.CreateVersion7();

            Name = name;
            PollId = pollId;
        }
    }
}