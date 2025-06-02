using MonopolyStorage.DataAccess.Database.Context;
using MonopolyStorage.Domain.Repositories.Entities;
using MonopolyStorage.Domain.Repositories.Interfaces;

namespace MonopolyStorage.DataAccess.Database.Repositories
{
    public class BoxRepository(ApplicationDbContext context) : GenericRepository<Guid, BoxEntity>(context), IBoxRepository;
}
