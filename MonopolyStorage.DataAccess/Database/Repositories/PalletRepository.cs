using MonopolyStorage.DataAccess.Database.Context;
using MonopolyStorage.Domain.Repositories.Entities;
using MonopolyStorage.Domain.Repositories.Interfaces;

namespace MonopolyStorage.DataAccess.Database.Repositories
{
    public class PalletRepository(ApplicationDbContext context) : GenericRepository<Guid, PalletEntity>(context), IPalletRepository;
}
