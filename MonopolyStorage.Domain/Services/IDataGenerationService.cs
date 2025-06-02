using MonopolyStorage.Domain.Models;

namespace MonopolyStorage.Domain.Services
{
    public interface IDataGenerationService
    {
        Box GenerateBox(double maxDimension = 100);
        Pallet GeneratePallet(int numberOfBoxes, double maxDimension = 1000);
    }
}
