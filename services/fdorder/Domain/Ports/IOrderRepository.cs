using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.Entities;

namespace fdorder.Domain.Ports
{
    public interface IOrderRepository
    {
        Task<bool> InsertOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> DeleteOrder(OrderId orderId);
        Task<Order> GetOrder(OrderId orderId);
        Task<IEnumerable<Order>> GetAll<Order>(Func<Order, bool> predicate);
    }
}