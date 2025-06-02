using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Pallets
{
    public class GetPalletsDataFromFileCommand : Command
    {
        public GetPalletsDataFromFileCommand() : base("get_pallets_file", "Получить данные из файла") 
        {
            var palletPathOption = new Option<string>(name: "--pallet-path", description: "Путь к файлу с паллетами")
            {
                IsRequired = true
            };
            AddOption(palletPathOption);
        }

        public class Handler(IPalletService palletService, IBoxService boxService, CommandsCacheStorage storage) : ICommandHandler
        {
            public required string PalletPath { get; set; }
            public int Invoke(InvocationContext context)
            {
                var pallets = palletService.GetFromFile(PalletPath);
                storage.AddPallets(pallets);
                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
