using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.General
{
    public class ShowCommand(CommandsCacheStorage storage) : Command("show", "Показать коллекцию")
    {
        public override void Execute()
        {
            Console.WriteLine("Паллеты:");
            var pallets = storage.GetAllPallets();
            foreach(var pallet in pallets)            
                Console.WriteLine(pallet.ToString());         

            Console.WriteLine("Коробки:");
            var boxes = storage.GetAllBoxes();
            foreach (var box in boxes)
                Console.WriteLine(box.ToString());
        }
    }
}
