using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Boxes
{
    public class GenerateBoxCommand : Command
    {
        public GenerateBoxCommand() : base("generate_box", "Сгенерировать коробку")
        {
            var maxDimensionOption = new Option<int?>("--max-dim", "Верхняя граница генерации размеров. По умолчанию - 100")
            {
                IsRequired = false
            };
            AddOption(maxDimensionOption);
        }
        public class Handler(IDataGenerationService generationService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int? MaxDimension { get; set; }
            public int Invoke(InvocationContext context)
            {
                var box = (MaxDimension.HasValue)
                ? generationService.GenerateBox(MaxDimension.Value)
                : generationService.GenerateBox();
                storage.AddBox(box);

                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}

