using MonopolyStorage.Domain.Models;

namespace MonopolyStorage.Domain.Services.Implementations
{
    public class DataGenerationService : IDataGenerationService
    {
        private readonly Random _random;
        public DataGenerationService ()
        {
            _random = new Random ();
        }

        public Box GenerateBox(double maxDimension = 100)
        {
            var width = _random.NextDouble() * maxDimension;
            var height = _random.NextDouble() * maxDimension;
            var depth = _random.NextDouble() * maxDimension;
            var weight = _random.NextDouble() * maxDimension;

            var start = DateTime.UtcNow.AddDays(-100);
            var productionDateTime = start.AddDays(_random.Next(100));
            return Box.CreateWithProductionDate(Guid.NewGuid(), width, depth, height, weight, DateOnly.FromDateTime(productionDateTime));
        }

        public Pallet GeneratePallet(int numberOfBoxes, double maxDimension = 1000)
        {
            var _random = new Random();
            var width = _random.NextDouble() * maxDimension;
            var height = _random.NextDouble() * maxDimension;
            var depth = _random.NextDouble() * maxDimension;

            var pallet = new Pallet(Guid.NewGuid(), width, height, depth);
            if (numberOfBoxes > 0) 
            {
                for (int i = 0; i < numberOfBoxes; i++)
                {                    
                    pallet.AddBoxOnPallet(GenerateBox(Math.Min(width, Math.Min(height, depth))));
                }
            }
            return pallet;
        }
    }
}
