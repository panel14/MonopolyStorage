namespace MonopolyStorage.Domain.Services
{
    public interface IDataIOService
    {
        IEnumerable<T> ReadEntitiesFromFile<T>(string filePath) where T : new();
    }
}
