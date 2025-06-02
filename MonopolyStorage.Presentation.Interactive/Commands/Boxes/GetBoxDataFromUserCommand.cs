using MonopolyStorage.Domain.Models;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;
using MonopolyStorage.Presentation.Interactive.Interactive.Readers;

namespace MonopolyStorage.Presentation.Interactive.Commands.Boxes
{
    public class GetBoxesDataFromUserCommand(CommandsCacheStorage storage) : Command("input_box", "Ввести коробки паллеты вручную")
    {
        public override void Execute()
        {
            Console.WriteLine("Ввод коробки. Введите параметры коробки ниже:");

            double width;
            double height;
            double depth;
            double weight;

            DateOnly expirationDate;
            DateOnly? expiration = null;

            DateOnly productionDate;
            DateOnly? production = null;
            Guid palleteId;

            Console.WriteLine("Ширина: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out width, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Ширина должна быть числом, больше нуля");

            Console.WriteLine("Высота: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out height, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Высота должна быть числом, больше нуля");

            Console.WriteLine("Глубина: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out depth, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Глубина должна быть числом, больше нуля");

            Console.WriteLine("Вес: ");
            while (!DialogReader.TryReadDouble(Console.ReadLine(), out weight, (x) => x > 0))
                Console.WriteLine("Неверный формат ввода. Вес должен быть числом, больше нуля");

            Console.WriteLine("Срок годности. Данное поле необязательно для ввода, " +
                "если будет введена дата производства (для пропуска ввода нажмите Enter):");
            var expirationString = Console.ReadLine();
            if (!string.IsNullOrEmpty(expirationString))
            {
                while (!DialogReader.TryReadDateOnly(expirationString, out expirationDate))
                {
                    Console.WriteLine("Неверный формат ввода. Введите дату в формате DD.MM.YYYY");
                    expirationString = Console.ReadLine();
                }
                expiration = expirationDate;
            }

            Console.WriteLine("Дата производства. Данное поле необязательно для ввода, если был введен" +
                "срок годности. (для пропуска ввода нажмите Enter):");
            var productionString = Console.ReadLine();

            if (!string.IsNullOrEmpty(productionString) || string.IsNullOrEmpty(expirationString))
            {
                while (!DialogReader.TryReadDateOnly(productionString, out productionDate))
                {
                    Console.WriteLine("Неверный формат ввода. Введите дату в формате DD.MM.YYYY");
                    productionString = Console.ReadLine();
                }
                production = productionDate;
            }

            Console.WriteLine("Паллета. Введите ID паллеты, на которой будет стоять коробка:");
            while (!Guid.TryParse(Console.ReadLine(), out palleteId))
                Console.WriteLine("Неверый формат ввода. Введите GUID: 00000000-0000-0000-0000-000000000000");

            Box box = production.HasValue
                ? Box.CreateWithProductionDate(width, depth, height, weight, production.Value)
                : Box.CreateWithExpirationDate(width, depth, height, weight, expiration!.Value);

            storage.MergeBox(palleteId, box);
        }
    }
}
