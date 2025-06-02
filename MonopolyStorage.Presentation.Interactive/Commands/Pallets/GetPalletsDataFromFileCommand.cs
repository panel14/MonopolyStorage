using MonopolyStorage.Domain.Services;
using MonopolyStorage.Infrastructure.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GetPalletsDataFromFileCommand : Command
    {
        private readonly IPalletService _palletService;
        private readonly CommandsCacheStorage _storage;

        public string PalletPath { get; set; } = null!;
        public string? BoxPath { get; set; }

        public GetPalletsDataFromFileCommand(IPalletService palletService, IBoxService boxService, CommandsCacheStorage storage) : base("get_pallets_file", "Получить данные из файла") 
        {
            var palletPathOption = new Option<string>(name: "--pallet-path", description: "Путь к файлу с паллетами")
            {
                IsRequired = true
            };
            var boxPathOption = new Option<string>(name: "--box-path", "Путь к файлу с коробками")
            {
                IsRequired = false
            };
            Options.Add(palletPathOption);
            Options.Add(boxPathOption);

            _palletService = palletService;
            _storage = storage;
        }

        public override void Execute()
        {
            var pallets = _palletService.GetFromFile(PalletPath, BoxPath);
            _storage.AddPallets(pallets);
        }
    }
}
