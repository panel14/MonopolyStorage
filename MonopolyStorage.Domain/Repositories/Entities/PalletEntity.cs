using MonopolyStorage.DataAccess.Entities.Base;

namespace MonopolyStorage.Domain.Repositories.Entities
{
    public class PalletEntity : EntityBase<Guid>
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public DateOnly? ProductionDate { get; set; }

        public List<BoxEntity> Boxes { get; set; } = [];
    }
}
