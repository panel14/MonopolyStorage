using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;
using MonopolyStorage.Domain.Repositories.Interfaces;
using MonopolyStorage.Domain.Services;

namespace MonopolyStorage.Infrastructure.Services
{
    public class PalletService(IDataGenerationService generationService, IPalletRepository repository,
        IDataIOService iOService, IPalletMapper mapper)
        : IPalletService
    {

        public Pallet GeneratePallet(int numberOfBoxes)
        {
            return generationService.GeneratePallet(numberOfBoxes);
        }

        public IEnumerable<Pallet> Get3OldestPallet(IEnumerable<Pallet> pallets)
        {
            return pallets.OrderBy(p => p.GetBoxes()
            .OrderByDescending(b => b.ExpirationDate)
            .FirstOrDefault()?.ExpirationDate)
            .OrderByDescending(p => p.Volume).Take(3);
        }

        public async Task<IEnumerable<Pallet>> GetFromDB()
        {
            var palletsEntities = await repository.GetAllAsync(includeProperties: ["Boxes"]);
            return palletsEntities.Select(mapper.MapFromDb);
        }

        public IEnumerable<Pallet> GetFromFile(string palletsFilePath, string? boxesFilePath = null)
        {
            var palletsEntities = iOService.ReadEntitiesFromFile<PalletEntity>(palletsFilePath);

            if (!string.IsNullOrEmpty(boxesFilePath))
            {
                var boxEntities = iOService.ReadEntitiesFromFile<BoxEntity>(boxesFilePath);
                var gBoxes = boxEntities.Where(b => b.PalletId.HasValue)
                    .GroupBy(b => b.PalletId!.Value);
                palletsEntities = palletsEntities.Join(gBoxes, p => p.Id, gB => gB.Key,
                    (p, gB) => new PalletEntity
                    {
                        Id = p.Id,
                        Width = p.Width,
                        Height = p.Height,
                        Depth = p.Depth,
                        Volume = p.Volume,
                        Weight = p.Weight,
                        ExpirationDate = p.ExpirationDate,
                        ProductionDate = p.ProductionDate,
                        Boxes = [.. gB]
                    });
            }           
            return palletsEntities.Select(mapper.MapFromDb);
        }

        public Dictionary<DateOnly, IOrderedEnumerable<Pallet>> GroupByCondition(IEnumerable<Pallet> pallets)
        {
            return pallets.Where(p => p.ExpirationDate.HasValue)
                .GroupBy(p => p.ExpirationDate.Value)
                .Select(g => new
                {
                    ExpirationDate = g.Key,
                    Pallets = g.OrderByDescending(x => x.Weight)
                })
                .OrderBy(g => g.ExpirationDate)
                .ToDictionary(k => k.ExpirationDate, v => v.Pallets);
        }
    }
}
