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
        Task<bool> InsertOrder(DOrder order);
        Task<DOrder> UpdateOrder(DOrder order);
        Task<DOrder> DeleteOrder(OrderId orderId);
        Task<DOrder> GetOrder(OrderId orderId);
        Task<IEnumerable<Order>> GetAll<Order>(Func<Order, bool> predicate);
    }
}