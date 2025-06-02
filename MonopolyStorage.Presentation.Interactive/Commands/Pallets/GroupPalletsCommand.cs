using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GroupPalletsCommand(IPalletService palletService, CommandsCacheStorage storage) 
        : Command("group_pallets", "Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.")
    {
        public override void Execute()
        {
            var result = palletService.GroupByCondition(storage.GetAllPallets());
            int i = 0;
            foreach (var pallet in result)
            {
                i++;
                Console.WriteLine($"Группа {i}; Срок годности ({pallet.Key})");
                Console.WriteLine("---");
                foreach(var p in pallet.Value)
                {
                    Console.WriteLine(p.ToString());
                }
            }

        }
    }
}
