using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands
{
    public class ShowCommand : Command
    {
        public ShowCommand() : base("show", "Показать коллекцию"){}

        public class Handler(CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
            {
                var boxes = storage.GetAllBoxes();
                foreach(var box in boxes)
                {
                    Console.WriteLine(box.ToString());
                }
                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
