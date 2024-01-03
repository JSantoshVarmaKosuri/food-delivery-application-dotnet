using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;

namespace fdorder.Domain.Entities
{
    public class Order : BaseEntity<OrderId>
    {
        public Order(
            OrderId orderId,
            CustomerId customerId,
            RestarentId restarentId,
            List<OrderItem> orderItems,
            OrderStatus orderStatus
        ) : base(orderId)
        {
            CustomerId = customerId;
            RestarentId = restarentId;
            OrderItems = orderItems;
            OrderStatus = orderStatus;
            Price calculatedPrice = new Price(Price.Zero);
            orderItems.ForEach((OrderItem orderItem) =>
            {
                calculatedPrice.addMount(orderItem.SubTotal.Amount);
            });
            Price = calculatedPrice;
        }

        public CustomerId CustomerId { get; }
        public RestarentId RestarentId { get; }
        public PaymentId PaymentId { get; set; }
        public List<OrderItem> OrderItems { get; }
        public OrderStatus OrderStatus { get; set; }
        public Price Price { get; }
    }
}