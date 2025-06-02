using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Boxes
{
    public class GenerateBoxCommand : Command
    {
        private IDataGenerationService _generationService { get; set; }
        private CommandsCacheStorage _storage { get; set; }

        public int? MaxDimension { get; set; }
        public GenerateBoxCommand(IDataGenerationService generationService, CommandsCacheStorage storage) 
            : base("generate_box", "Сгенерировать коробку")
        {
            var maxDimensionOption = new Option<int>("--max-dimension", "Верхняя граница генерации размеров. По умолчанию - 100")
            {
                IsRequired = false
            };

            Options.Add(maxDimensionOption);

            _generationService = generationService;
            _storage = storage;
        }

        public override void Execute()
        {
            var box = (MaxDimension.HasValue)
                ? _generationService.GenerateBox(MaxDimension.Value)
                : _generationService.GenerateBox();
            _storage.AddBox(box);
        }
    }
}
