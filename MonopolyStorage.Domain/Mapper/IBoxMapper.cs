using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.Domain.Mapper
{
    public interface IBoxMapper
    {
        Box MapFromDb(BoxEntity entity);
    }
}
