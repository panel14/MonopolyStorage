namespace MonopolyStorage.DataAccess.IO.Options
{
    public enum FormatType
    {
        JSON = 0,
        CSV = 1,
    }

    public class DataIOOptions
    {
        public required FormatType FormatType { get; set; }
        public char CSVDataDelimiter { get; set; } = ',';
        public string[]? CSVHeaders { get; set; } = null;
    }
}
