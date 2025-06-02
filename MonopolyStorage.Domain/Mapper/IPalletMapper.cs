using MonopolyStorage.Domain.Models;
using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.Domain.Mapper
{
    public interface IPalletMapper
    {
        Pallet MapFromDb(PalletEntity entity);
    }
}
