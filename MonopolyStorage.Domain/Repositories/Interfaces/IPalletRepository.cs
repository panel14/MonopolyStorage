using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.Domain.Repositories.Interfaces
{
    public interface IPalletRepository : IGenericRepository<Guid, PalletEntity>;
}
