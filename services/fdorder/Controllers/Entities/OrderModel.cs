using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdcommon.Domain.Dtos.Order;

namespace fdorder.Controllers.Entities
{
    public class OrderModel
    {
        public Guid customerId { get; set; }
        public Guid restarentId { get; set; }
        public List<OrderItemDto> orderItems { get; set; } = [];
    }
}