
using System.Linq;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.Entities;
using fdorder.Domain.Ports;

namespace fdorder.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext db;
        public OrderRepository(OrderContext orderContext)
        {
            this.db = orderContext;
        }

        public Task<Order> DeleteOrder(OrderId orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll<Order>(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrder(OrderId orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrder(Order order)
        {
            try
            {
                fdorder.DataAccess.Entities.Order dbOrder = new fdorder.DataAccess.Entities.Order();
                dbOrder.OrderId = order.Id.Id;
                dbOrder.RestarentId = order.RestarentId.Id;
                dbOrder.CustomerId = order.CustomerId.Id;
                dbOrder.PaymentId = null;
                dbOrder.Status = (byte)order.OrderStatus;
                dbOrder.Price = order.Price.Amount;
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    fdorder.DataAccess.Entities.OrderItem newOrderItem = new fdorder.DataAccess.Entities.OrderItem();
                    foreach (Dish dish in orderItem.Dishes)
                    {
                        fdorder.DataAccess.Entities.Dish newDish = new fdorder.DataAccess.Entities.Dish();
                        newDish.DishId = dish.Id.Id;
                        newDish.OrderItemId = dish.OrderItemId.Id;
                        newDish.Name = dish.Name;
                        newDish.Price = dish.Price.Amount;
                        newDish.Quantity = dish.Quantity;
                        newOrderItem.Dishes.Add(newDish);
                    }

                    newOrderItem.OrderId = orderItem.OrderId.Id;
                    newOrderItem.OrderItemId = orderItem.Id.Id;
                    dbOrder.OrderItems.Add(newOrderItem);
                }
                
                await this.db.Orders.AddAsync(dbOrder);

                await this.db.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}