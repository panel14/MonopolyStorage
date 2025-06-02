namespace MonopolyStorage.Presentation.Interactive.Commands.Base
{
    public abstract class Option(string name, string description)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public bool IsRequired { get; set; } = false;
    }
}
