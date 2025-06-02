using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Boxes
{
    public class GetBoxesDataFromFileCommand : Command
    {
        public GetBoxesDataFromFileCommand() 
            : base ("get_boxes_file", "Получить данные из файла")
        {
            var boxLoadOption = new Option<string>(name: "--box-path", description: "Путь к файлу с коробками")
            {
                IsRequired = true
            };
            AddOption(boxLoadOption);
        }

        public class Handler(IBoxService boxService, CommandsCacheStorage storage) : ICommandHandler
        {
            public required string BoxPath { get; set; }

            public int Invoke(InvocationContext context)
            {
                var boxes = boxService.GetFromFile(BoxPath);
                storage.AddBoxes(boxes);

                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
