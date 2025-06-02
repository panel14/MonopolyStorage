using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Pallets
{
    public class GeneratePalletCommand : Command
    {
        private readonly IDataGenerationService _generationService;
        private readonly CommandsCacheStorage _storage;

        public int? MaxDimension { get; set; }
        public int BoxNumber { get; set; }

        public GeneratePalletCommand(IDataGenerationService generationService, CommandsCacheStorage storage) : base("generate_pallet", "Сгенерировать паллету")
        {
            var maxDimensionOption = new Option<int?>("--max-dimension", "Верхняя граница генерации размеров. По умолчанию - 1000")
            {
                IsRequired = false
            };
            var numberOfBoxOption = new Option<int>("--box-number", "Количество коробок на паллете, которое будет сгенерировано.")
            {
                IsRequired = true
            };
            Options.Add(maxDimensionOption);
            Options.Add(numberOfBoxOption);

            _generationService = generationService;
            _storage = storage;
        }

        public override void Execute()
        {
            var pallet = MaxDimension.HasValue
                ? _generationService.GeneratePallet(BoxNumber, MaxDimension.Value)
                : _generationService.GeneratePallet(BoxNumber);
            _storage.AddPallet(pallet);
        }
    }
}
