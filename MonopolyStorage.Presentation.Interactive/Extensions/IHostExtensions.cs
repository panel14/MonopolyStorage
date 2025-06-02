using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.Utils;

namespace MonopolyStorage.Presentation.Interactive.Extensions
{
    public static class IHostExtensions
    {
        public static void StartInteractive(this IHost host)
        {
            var commands = host.Services.GetServices<Command>()
                .ToDictionary(c => c.Name, c => c);
            var executer = new CommandExecuter(commands);

            Console.WriteLine("Добро пожаловать!\n" +
                "Введите -h для вывода списка комад и их описания\n" +
                "Введите <command> -h для вывода информации по команде:");
            string? input;
            while ((input = Console.ReadLine()) != "exit")
            {
                SafetyWrapper.SafetyExecuteCommand(() =>
                {
                    executer.Invoke(input);
                });
            }
        }
    }
}
