using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Boxes
{
    public class GetBoxesDataFromDBCommand : Command
    {
        public GetBoxesDataFromDBCommand()
            : base ("get_boxes_db", "Получает данные из базы данных")
        {
        }

        public class Handler(IBoxService boxService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
            {
                var boxes = Task.Run(async () => await boxService.GetFromDB())
                    .GetAwaiter()
                    .GetResult();
                storage.AddBoxes(boxes);

                return 0;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                var boxes = await boxService.GetFromDB();
                storage.AddBoxes(boxes);

                return 0;
            }
        }
    }
}
