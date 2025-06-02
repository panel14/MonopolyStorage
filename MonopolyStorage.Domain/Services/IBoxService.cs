using MonopolyStorage.Domain.Models;

namespace MonopolyStorage.Domain.Services
{
    public interface IBoxService
    {
        Task<IEnumerable<Box>> GetFromDB();
        IEnumerable<Box> GetFromFile(string filePath);
    }
}
