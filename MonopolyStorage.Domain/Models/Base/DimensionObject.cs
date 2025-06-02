namespace MonopolyStorage.Domain.Models.Base
{
    public abstract class DimensionObject(double width, double depth, double height)
    {
        private double _width = width;
        private double _depth = depth;
        private double _height = height;

        public double Width 
        {
            get => _width;
            set => _width = ValidateDimension(value, nameof(Width));
        }

        public double Height
        {
            get => _height;
            set => _height = ValidateDimension(value, nameof(Height));
        }

        public double Depth
        {
            get => _depth;
            set => _depth = ValidateDimension(value, nameof(Depth));
        }

        public abstract double Volume { get; }

        private static double ValidateDimension(double value, string propertyName)
        {
            if (value <= 0)            
                throw new ArgumentOutOfRangeException($"{propertyName} must be greater than 0. Given {value}");
            
            return value;
        }
    }
}
