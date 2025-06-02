using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Pallets
{
    public class GroupPalletsCommand : Command
    {
        public GroupPalletsCommand() 
            : base("group_pallets", "Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.") { }

        public class Handler(IPalletService palletService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
            {
                var result = palletService.GroupByCondition(storage.GetAllPallets());
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
