namespace MonopolyStorage.Presentation.Interactive.Commands.Base
{
    public class Option<T>(string name, string description) : Option(name, description)
    {
        public T? Value { get; set; }
    }
}
