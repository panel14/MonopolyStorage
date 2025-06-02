namespace MonopolyStorage.Presentation.Interactive.Readers
{
    public static class DialogReader
    {
        public static bool TryReadDouble(string rawValue, out double result, Func<double, bool>? validator = null)
        {
            result = double.NaN;
            if (double.TryParse(rawValue, out var value))
            {
                if (validator == null || validator(value))
                {
                    result = value;
                    return true;
                }
            }
            return false;
        }

        public static bool TryReadDateOnly(string rawValue, out DateOnly result, Func<DateOnly, bool>? validator = null)
        {
            result = new DateOnly();
            if (DateOnly.TryParse(rawValue, out var value))
            {
                if (validator == null || validator(value))
                {
                    result = value;
                    return true;
                }
            }
            return false;
        }
    }
}
