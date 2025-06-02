using MonopolyStorage.Domain.Models;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;
using MonopolyStorage.Presentation.Interactive.Interactive.Readers;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GetPalletDataFromUser(CommandsCacheStorage storage) 
        : Command("input_pallet", "Ввести данные паллеты вручную")
    {
        public override void Execute()
        {
            Console.WriteLine("Ввод паллета. Введите параметры паллета ниже:");

            double width;
            double height;
            double depth;

            Console.WriteLine("Ширина: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out width, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Ширина должна быть числом, больше нуля");

            Console.WriteLine("Высота: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out height, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Высота должна быть числом, больше нуля");

            Console.WriteLine("Глубина: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out depth, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Глубина должна быть числом, больше нуля");

            storage.AddPallet(new Pallet(width, depth, height));
        }
    }
}
