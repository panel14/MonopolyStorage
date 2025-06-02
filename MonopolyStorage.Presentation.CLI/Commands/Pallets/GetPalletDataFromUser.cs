using MonopolyStorage.Domain.Models;
using MonopolyStorage.Presentation.CommandsCache;
using MonopolyStorage.Presentation.Interactive.Readers;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Boxes
{
    public class GetPalletDataFromUser : Command
    {
        public GetPalletDataFromUser() : base("input_pallet", "Ввести данные паллеты вручную") { }

        public class Handler(CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
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
                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
