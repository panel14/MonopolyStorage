namespace MonopolyStorage.DataAccess.Entities.Base
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
