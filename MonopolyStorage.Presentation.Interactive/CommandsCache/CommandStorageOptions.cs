namespace MonopolyStorage.Presentation.Interactive.CommandsCache
{
    public enum SimilarBehavior
    {
        RAISE_EXCEPTION = 0,
        REPLACE = 1
    }

    public class CommandStorageOptions
    {
        public SimilarBehavior SimilarPalletsBehavior { get; set; }
        public SimilarBehavior SimilarBoxesBehavior { get; set; }
    }
}
