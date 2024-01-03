using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class DOrderItem : BaseEntity<OrderItemId>
    {
        public DOrderItem(
            OrderItemId orderItemId,
            OrderId orderId,
            List<DDish> dishes
        ) : base(orderItemId)
        {
            OrderId = orderId;
            Dishes = dishes;
            Price calculatedSubTotal = new Price(Price.Zero);
            dishes.ForEach((DDish dish) =>
            {
                calculatedSubTotal.addMount(dish.Price.getAmountPerQuantity(dish.Quantity));
            });
            SubTotal = calculatedSubTotal;
        }

        public OrderId OrderId { get; set; }
        public List<DDish> Dishes { get; set; }
        public Price SubTotal { get; set; }
    }
}