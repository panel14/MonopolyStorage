using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.Infrastructure.Mapper
{
    public class PalletMapper : IPalletMapper
    {
        public Pallet MapFromDb(PalletEntity entity)
        {
            var boxes = entity.Boxes.Select(b =>
            {
                if (b.ProductionDate.HasValue)
                    return Box.CreateWithProductionDate(b.Id, b.Width, b.Depth, b.Height, b.Weight, b.ProductionDate.Value);
                return Box.CreateWithExpirationDate(b.Id, b.Width, b.Depth, b.Height, b.Weight, b.ExpirationDate!.Value);
            });
            return new Pallet(entity.Id, entity.Width, entity.Depth, entity.Height, boxes);
        }
    }
}
