namespace fdcommon.Domain.Entities
{
    public class BaseEntity<T>
    {
        public BaseEntity(T id)
        {
            Id = id;
        }

        public T Id { get; set; }
    }
}