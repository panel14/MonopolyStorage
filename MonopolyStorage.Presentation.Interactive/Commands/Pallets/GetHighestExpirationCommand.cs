using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GetHighestExpirationCommand(IPalletService palletService, CommandsCacheStorage storage) 
        : Command("get_3_pallets", "3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.")
    {
        public override void Execute()
        {
            var result = palletService.Get3OldestPallet(storage.GetAllPallets());
            foreach (var pallet in result)
                Console.WriteLine(pallet.ToString());
        }
    }
}
