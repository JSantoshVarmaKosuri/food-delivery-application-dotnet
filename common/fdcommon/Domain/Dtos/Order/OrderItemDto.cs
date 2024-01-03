namespace fdcommon.Domain.Dtos.Order
{
    public class OrderItemDto
    {
        public OrderItemDto(
            Guid orderItemId,
            Guid orderId,
            List<DishDto> dishes,
            decimal subTotal
        )
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            Dishes = dishes;
            SubTotal = subTotal;
        }

        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public List<DishDto> Dishes { get; set; }
        public decimal SubTotal { get; set; }
    }
}