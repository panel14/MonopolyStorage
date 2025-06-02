using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.CommandsCache;

namespace MonopolyStorage.Presentation.Interactive.Commands.Boxes
{
    public class GetBoxesDataFromFileCommand : Command
    {
        private readonly IBoxService _boxService;
        private readonly CommandsCacheStorage _storage;

        public string BoxPath { get; set; } = null!;

        public GetBoxesDataFromFileCommand(IBoxService boxService, CommandsCacheStorage storage) 
            : base ("get_boxes_file", "Получить данные из файла")
        {
            var boxLoadOption = new Option<string>(name: "--box-path", description: "Путь к файлу с коробками")
            {
                IsRequired = true
            };
            Options.Add(boxLoadOption);
            _boxService = boxService;
            _storage = storage;
        }

        public override void Execute()
        {
            var boxes = _boxService.GetFromFile(BoxPath);
            _storage.AddBoxes(boxes);
        }
    }
}
