using MonopolyStorage.Domain.Repositories.Entities;

namespace MonopolyStorage.Domain.Repositories.Interfaces
{
    public interface IBoxRepository : IGenericRepository<Guid, BoxEntity>;
}
