namespace MonopolyStorage.Presentation.Interactive.Commands.Base
{
    public abstract class Command(string name, string description)
    {
        public List<Option> Options = [];
        public required string Name { get; set; } = name;
        public required string Description { get; set; } = description;
        public abstract void Execute();
    }
}
