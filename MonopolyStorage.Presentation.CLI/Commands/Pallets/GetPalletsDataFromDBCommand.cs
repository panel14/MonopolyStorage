using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Pallets
{
    public class GetPalletsDataFromDBCommand : Command
    {
        public GetPalletsDataFromDBCommand() : base("get_pallets_db", "Получает данные из базы данных") { }

        public class Handler(IPalletService palletService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int Invoke(InvocationContext context)
            {
                var pallets = Task.Run(async () => await palletService.GetFromDB())
                    .GetAwaiter()
                    .GetResult();
                storage.AddPallets(pallets);

                return 0;
            }

            public async Task<int> InvokeAsync(InvocationContext context) 
            { 
            
                var pallets = await palletService.GetFromDB();
                storage.AddPallets(pallets);

                return 0;
            }
        }
    }
}
