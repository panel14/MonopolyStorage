using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Pallets
{
    public class GetHighestExpirationCommand : Command
    {
        public GetHighestExpirationCommand() 
            : base ("get_3_pallets", "3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.") {}

        public class Handler(IPalletService palletService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
            {
                var result = palletService.Get3OldestPallet(storage.GetAllPallets());
                foreach (var pallet in result)
                    Console.WriteLine(pallet);

                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
