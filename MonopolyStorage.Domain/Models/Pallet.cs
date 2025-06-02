using MonopolyStorage.Domain.Models.Base;

namespace MonopolyStorage.Domain.Models
{
    public class Pallet(Guid id, double width, double depth, double height) : DimensionObject(width, depth, height)
    {
        public Pallet(double width, double depth, double height)
            : this(Guid.NewGuid(), width, depth, height) { }

        public Pallet(Guid id, double width, double depth, double height, IEnumerable<Box> boxes)
            : this (id, width, depth, height)
        {
            if (boxes.Any(x => !CanAddBox(x)))
                throw new ArgumentException("Коробка не может быть по размерам больше, чем паллета.");
            _boxes = [.. boxes];
        }

        private static readonly int _defaultWeight = 30;
        private readonly List<Box> _boxes = [];

        public Guid Id { get; init; } = id;
        public override double Volume => _boxes.Sum(b => b.Volume) + Width * Height * Depth;
        public DateOnly? ExpirationDate => _boxes.Count == 0 
            ? null
            : _boxes.Min(b => b.ExpirationDate).GetValueOrDefault();
        public double Weight => _boxes.Sum(b => b.Weight) + _defaultWeight;

        public IReadOnlyCollection<Box> GetBoxes() => _boxes.AsReadOnly();

        public bool CanAddBox(Box box) => box.Width <= Width && box.Depth <= Depth;

        public void AddBoxOnPallet(Box box)
        {
            if (!CanAddBox(box)) 
            {
                throw new ArgumentException("Коробка не может быть по размерам больше, чем паллета.");
            }
            _boxes.Add(box);
        }

        public override string ToString()
        {
            var boxesStrings = _boxes.Select(x => x.ToString());

            var objectString = $"--- Паллета (ID: {Id}) ---\n" +
                $"Ширина: {Width,-15}\n" +
                $"Высота: {Height,-15}\n" +
                $"Глубина: {Depth,-15}\n" +
                $"Вес: {Weight,-15}\n" +
                $"Объем: {Volume,-15}\n" +
                $"Срок годности: {ExpirationDate,-15}\n" +
                string.Join('\n', boxesStrings);
                
            return objectString;
        }
    }
}
