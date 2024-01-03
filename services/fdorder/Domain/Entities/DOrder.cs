using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class DOrder : BaseEntity<OrderId>
    {
        public DOrder(
            OrderId orderId,
            CustomerId customerId,
            RestarentId restarentId,
            List<DOrderItem> orderItems,
            OrderStatus orderStatus
        ) : base(orderId)
        {
            CustomerId = customerId;
            RestarentId = restarentId;
            OrderItems = orderItems;
            OrderStatus = orderStatus;
            Price calculatedPrice = new Price(Price.Zero);
            orderItems.ForEach((DOrderItem orderItem) =>
            {
                calculatedPrice.addMount(orderItem.SubTotal.Amount);
            });
            Price = calculatedPrice;
        }

        public CustomerId CustomerId { get; }
        public RestarentId RestarentId { get; }
        public PaymentId PaymentId { get; set; }
        public List<DOrderItem> OrderItems { get; }
        public OrderStatus OrderStatus { get; set; }
        public Price Price { get; }
    }
}