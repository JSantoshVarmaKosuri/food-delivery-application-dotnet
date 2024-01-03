using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fdcommon.Domain.Dtos.Order
{
    public class DishDto
    {
        public DishDto(
            Guid dishId,
            Guid orderItemId,
            string name,
             Int16 quantity,
            decimal price
        )
        {
            DishId = dishId;
            OrderItemId = orderItemId;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Guid DishId { get; set; }
        public Guid OrderItemId { get; set; }
        public string Name { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
    }
}