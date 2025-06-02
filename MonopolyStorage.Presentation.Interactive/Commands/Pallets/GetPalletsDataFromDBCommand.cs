using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GetPalletsDataFromDBCommand(IPalletService palletService, CommandsCacheStorage storage) 
        : Command("get_pallets_db", "Получает данные из базы данных")
    {
        public override void Execute()
        {
            var pallets = Task.Run(async () => await palletService.GetFromDB())
                .GetAwaiter()
                .GetResult();
            storage.AddPallets(pallets);
        }
    }
}
