namespace fdcommon.Domain.Entities
{
    public class AggregateRoot<T> : BaseEntity<T>
    {
        public AggregateRoot(T id) : base(id)
        {

        }
    }
}