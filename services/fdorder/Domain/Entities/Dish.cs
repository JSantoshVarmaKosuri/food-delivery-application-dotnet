using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class Dish : BaseEntity<DishId>
    {
        public Dish(
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

        public OrderItemId OrderItemId { get; }
        public string Name { get; }
        public short Quantity { get; set; }
        public Price Price { get; }
    }
}