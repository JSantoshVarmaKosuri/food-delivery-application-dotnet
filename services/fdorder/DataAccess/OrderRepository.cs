
using System.Linq;
using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.DataAccess.Entities;
using fdorder.Domain.Entities;
using fdorder.Domain.Ports;
using fdorder.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace fdorder.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext db;
        public OrderRepository(OrderContext orderContext)
        {
            this.db = orderContext;
        }

        public Task<DOrder> DeleteOrder(OrderId orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll<Order>(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<DOrder> GetOrder(OrderId orderId)
        {
            try
            {
                Order? dbOrder = await this.db.Orders
                .Include(a => a.OrderItems)
                .ThenInclude(b => b.Dishes)
                .Where<Order>((Order order) => order.OrderId.Equals(orderId.Id)).FirstOrDefaultAsync<Order>();
                if (dbOrder != null)
                {
                    List<DOrderItem> orderItems = new List<DOrderItem>();
                    foreach (OrderItem orderItem in dbOrder.OrderItems)
                    {
                        List<DDish> dishes = new List<DDish>();
                        foreach (Dish dish in orderItem.Dishes)
                        {
                            dishes.Add(
                                new DDish(
                                new DishId(Guid.Parse(dish.DishId.ToString())),
                                new OrderItemId(Guid.Parse(dish.OrderItemId.ToString())),
                                dish.Name,
                                dish.Quantity,
                                new Price(dish.Price)
                                )
                          );
                        }

                        orderItems.Add(
                            new DOrderItem(
                                new OrderItemId(Guid.Parse(orderItem.OrderItemId.ToString())),
                                new OrderId(Guid.Parse(orderItem.OrderId.ToString())),
                                dishes
                            )
                        );
                    }

                    OrderStatus orderStatus;
                    if (dbOrder.Status.ToString() == "1")
                    {
                        orderStatus = OrderStatus.PENDING;
                    }
                    else
                    {
                        orderStatus = OrderStatus.CANCELLED;
                    }

                    return new DOrder(
                        new OrderId(Guid.Parse(dbOrder.OrderId.ToString())),
                        new CustomerId(Guid.Parse(dbOrder.CustomerId.ToString())),
                        new RestarentId(Guid.Parse(dbOrder.RestarentId.ToString())),
                        orderItems,
                        orderStatus
                    );
                }
                else
                {
                    throw new Exception($"Order with OrderId: {orderId.Id} was not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw new Exception(e.Message.ToString());
            }
        }

        public async Task<bool> InsertOrder(DOrder order)
        {
            try
            {
                Order dbOrder = new Order();
                dbOrder.OrderId = order.Id.Id;
                dbOrder.RestarentId = order.RestarentId.Id;
                dbOrder.CustomerId = order.CustomerId.Id;
                dbOrder.PaymentId = null;
                dbOrder.Status = (byte)order.OrderStatus;
                dbOrder.Price = order.Price.Amount;
                foreach (DOrderItem orderItem in order.OrderItems)
                {
                    OrderItem newOrderItem = new OrderItem();
                    foreach (DDish dish in orderItem.Dishes)
                    {
                        Dish newDish = new Dish();
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

        public Task<DOrder> UpdateOrder(DOrder order)
        {
            throw new NotImplementedException();
        }
    }
}