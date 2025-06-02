using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;
namespace MonopolyStorage.Infrastructure.Mapper
{
    public class BoxMapper : IBoxMapper
    {
        public Box MapFromDb(BoxEntity entity)
        {
            if (entity.ProductionDate.HasValue)
                return Box.CreateWithProductionDate(entity.Id, entity.Width, entity.Depth, entity.Height, entity.Weight, entity.ProductionDate.Value);
            return Box.CreateWithExpirationDate(entity.Id, entity.Width, entity.Depth, entity.Height, entity.Weight, entity.ExpirationDate!.Value);
        }
    }
}
