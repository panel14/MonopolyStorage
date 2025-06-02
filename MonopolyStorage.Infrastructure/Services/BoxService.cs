using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;
using MonopolyStorage.Domain.Repositories.Interfaces;
using MonopolyStorage.Domain.Services;

namespace MonopolyStorage.Infrastructure.Services
{
    public class BoxService (IBoxRepository repository, IDataIOService iOService, IBoxMapper mapper) : IBoxService
    {
        public async Task<IEnumerable<Box>> GetFromDB()
        {
            var boxEntities = await repository.GetAllAsync();
            return boxEntities.Select(mapper.MapFromDb);
        }

        public IEnumerable<Box> GetFromFile(string filePath)
        {
            var boxEntities = iOService.ReadEntitiesFromFile<BoxEntity>(filePath);
            return boxEntities.Select(mapper.MapFromDb);
        }
    }
}
