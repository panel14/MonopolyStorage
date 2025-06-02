using MonopolyStorage.Domain.Models;

namespace MonopolyStorage.Domain.Services
{
    public interface IPalletService
    {
        Dictionary<DateOnly, IOrderedEnumerable<Pallet>> GroupByCondition(IEnumerable<Pallet> pallets);
        IEnumerable<Pallet> Get3OldestPallet(IEnumerable<Pallet> pallets);
        Pallet GeneratePallet(int numberOfBoxes);
        Task<IEnumerable<Pallet>> GetFromDB();
        IEnumerable<Pallet> GetFromFile(string palletsFilePath, string? boxesFilePath = null);
    }
}
