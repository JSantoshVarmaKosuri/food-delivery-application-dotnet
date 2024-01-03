using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class OrderItem : BaseEntity<OrderItemId>
    {
        public OrderItem(
            OrderItemId orderItemId,
            OrderId orderId,
            List<Dish> dishes
        ) : base(orderItemId)
        {
            OrderId = orderId;
            Dishes = dishes;
            Price calculatedSubTotal = new Price(Price.Zero);
            dishes.ForEach((Dish dish) =>
            {
                calculatedSubTotal.addMount(dish.Price.getAmountPerQuantity(dish.Quantity));
            });
            SubTotal = calculatedSubTotal;
        }

        public OrderId OrderId { get; }
        public List<Dish> Dishes { get; }
        public Price SubTotal { get; }
    }
}