namespace Quorum.Domain.Entities
{
    public class Option
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Option() { }

        public Option(string name)
        {
            Id = Guid.CreateVersion7();
            Name = name;
        }
    }
}