using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class DDish : BaseEntity<DishId>
    {
        public DDish(
            DishId dishId,
            OrderItemId orderItemId,
            string name,
            Int16 quantity,
            Price price
        ) : base(dishId)
        {
            OrderItemId = orderItemId;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public OrderItemId OrderItemId { get; set; }
        public string Name { get; set; }
        public short Quantity { get; set; }
        public Price Price { get; set; }
    }
}