using MonopolyStorage.Domain.Models.Base;

namespace MonopolyStorage.Domain.Models
{
    public class Box : DimensionObject
    {
        private const int _defaultExpiration = 100;

        private Box(Guid id, double width, double depth, double height, double weight,
        DateOnly productionDate) : base(width, depth, height)
        {
            Id = id;
            Weight = weight;
            ProductionDate = productionDate;
        }

        public static Box CreateWithProductionDate(double width, double depth, double height, double weight,
            DateOnly productionDate) => CreateWithProductionDate(Guid.NewGuid(), width, depth, height, weight, productionDate);
   
        public static Box CreateWithProductionDate(Guid id, double width, double depth, double height, double weight,
            DateOnly productionDate) => new(id, width, depth, height, weight, productionDate);

        public static Box CreateWithExpirationDate(double width, double depth, double height, double weight,
            DateOnly expirationDate) => CreateWithExpirationDate(Guid.NewGuid(), width, depth, height, weight, expirationDate);

        public static Box CreateWithExpirationDate(Guid id, double width, double depth, double height, double weight,
            DateOnly expirationDate) => new (id, width, depth, height, weight, expirationDate.AddDays(-_defaultExpiration));

        public Guid Id { get; init; }

        private DateOnly? _expirationDate;

        public DateOnly? ExpirationDate 
        { 
            get => (ProductionDate.HasValue)
                ? ProductionDate.Value.AddDays(_defaultExpiration)
                :_expirationDate;
            set => _expirationDate = value;
        }
        public DateOnly? ProductionDate { get; set; }
        public override double Volume => Width * Height * Depth;
        public double Weight { get; set; }

        public override string ToString()
        {
            var objectString = $"--- Коробка (ID: {Id}) ---\n" +
                $"Ширина: {Width, -15}\n" +
                $"Высота: {Height, -15}\n" +
                $"Глубина: {Depth, -15}\n" +
                $"Вес: {Weight, -15}\n" +
                $"Объем: {Volume,-15}\n" +
                $"Дата производства: {ProductionDate,-15}\n" +
                $"Срок годности: {ExpirationDate,-15}\n" +
                "--- ---";
            return objectString;
        }
    }
}
