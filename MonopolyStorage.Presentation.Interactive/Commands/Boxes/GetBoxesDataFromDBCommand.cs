using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Boxes
{
    public class GetBoxesDataFromDBCommand(IBoxService boxService, CommandsCacheStorage storage) : Command("get_boxes_db", "Получает данные из базы данных")
    {
        public override void Execute()
        {
            var boxes = Task.Run(async () => await boxService.GetFromDB())
                .GetAwaiter()
                .GetResult();
            storage.AddBoxes(boxes);
        }
    }
}
