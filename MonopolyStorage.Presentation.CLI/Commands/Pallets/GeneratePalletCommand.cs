using MonopolyStorage.Domain.Services;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MonopolyStorage.Presentation.Commands.Pallets
{
    public class GeneratePalletCommand : Command
    {
        public GeneratePalletCommand() : base("generate_pallet", "Сгенерировать паллету")
        {
            var maxDimensionOption = new Option<int?>("--max-dim", "Верхняя граница генерации размеров. По умолчанию - 1000")
            {
                IsRequired = false
            };
            var numberOfBoxOption = new Option<int>("--box-num", "Количество коробок на паллете, которое будет сгенерировано.")
            {
                IsRequired = true
            };
            AddOption(maxDimensionOption);
            AddOption(numberOfBoxOption);
        }

        public class Handler(IDataGenerationService generationService, CommandsCacheStorage storage) : ICommandHandler
        {
            public int? MaxDimension { get; set; }
            public int NumberOfBox { get; set; }
            public int Invoke(InvocationContext context)
            {
                var pallet = (MaxDimension.HasValue)
                    ? generationService.GeneratePallet(NumberOfBox, MaxDimension.Value)
                    : generationService.GeneratePallet(NumberOfBox);
                storage.AddPallet(pallet);
                return 0;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                return Task.FromResult(Invoke(context));
            }
        }
    }
}
