using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdcommon.Domain.Entities;

namespace fdcommon.Domain.Dtos.Order
{
    public class OrderDto
    {
        public OrderDto(
            Guid orderId,
            Guid customerId,
            Guid restarentId,
            List<OrderItemDto> dishes,
            decimal price,
            string orderStatus
        )
        {
            OrderId = orderId;
            CustomerId = customerId;
            RestarentId = restarentId;
            Dishes = dishes;
            Price = price;
            OrderStatus = orderStatus;
        }

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RestarentId { get; set; }
        public List<OrderItemDto> Dishes { get; set; }
        public decimal Price { get; set; }
        public string OrderStatus { get; set; }
    }
}