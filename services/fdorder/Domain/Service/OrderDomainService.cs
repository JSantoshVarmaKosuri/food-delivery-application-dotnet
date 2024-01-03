using fdcommon.Domain.Dtos.Order;
using fdcommon.Domain.Entities;
using fdcommon.Domain.ValueTypes;
using fdorder.Domain.Entities;
using fdorder.Domain.Ports;
using fdorder.Domain.ValueTypes;
using fdorder.Queue;

namespace fdorder.Domain.Service
{
    public class OrderDomainService : IOrderDomainService, IDisposable
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderEventPublisher _orderEventPublisher = new OrderEventPublisher();
        private readonly OrderMessageQueue _orderMessageQueue = new OrderMessageQueue();

        public OrderDomainService(
            IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
            this._orderEventPublisher.OrderCreated += this._orderMessageQueue.OnOrderCreated;

        }

        public Task<OrderDto> ApproveOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> CancelOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> CreateOrder(
            Guid customerId,
            Guid restarentId,
            List<OrderItemDto> orderItems
        )
        {
            try
            {
                Order order = CreateOrderFromDto(customerId, restarentId, orderItems);

                await this._orderRepository.InsertOrder(order);

                this._orderEventPublisher.PublishOnCreatedEvent(order);

                OrderDto orderDto = CreateOrderDtoFromOrder(order);

                return orderDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<OrderDto> PayOrder(Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> RejectOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        private static OrderDto CreateOrderDtoFromOrder(Order order)
        {
            return new OrderDto(
                                order.Id.Id,
                                order.CustomerId.Id,
                                order.RestarentId.Id,
                                order.OrderItems.Select((OrderItem orderItem) =>
                                {
                                    return new OrderItemDto(
                                        orderItem.Id.Id,
                                        orderItem.OrderId.Id,
                                        orderItem.Dishes.Select((Dish dish) =>
                                        {
                                            return new DishDto(
                                                dish.Id.Id,
                                                dish.OrderItemId.Id,
                                                dish.Name,
                                                dish.Quantity,
                                                dish.Price.Amount
                                            );
                                        }).ToList<DishDto>(),
                                        orderItem.SubTotal.Amount
                                    );
                                }).ToList<OrderItemDto>(),
                                order.Price.Amount,
                                order.OrderStatus.ToString()
                            );
        }

        private static Order CreateOrderFromDto(Guid customerId, Guid restarentId, List<OrderItemDto> orderItems)
        {
            OrderId orderId = new OrderId(Guid.NewGuid());
            List<OrderItem> newOrderItems = orderItems.Select((OrderItemDto orderItem) =>
            {
                OrderItemId newOrderItemId = new OrderItemId(Guid.NewGuid());

                List<Dish> newDishes = orderItem.Dishes.Select((DishDto dish) =>
                {
                    return new Dish(
                        new DishId(Guid.NewGuid()),
                        newOrderItemId,
                        dish.Name,
                        dish.Quantity,
                        new Price(decimal.Parse(dish.Price.ToString()))
                    );
                }).ToList<Dish>();

                return new OrderItem(
                    newOrderItemId,
                    orderId,
                    newDishes
                );
            }).ToList<OrderItem>();

            Order order = new Order(
                orderId,
                new CustomerId(customerId),
                new RestarentId(restarentId),
                newOrderItems,
                OrderStatus.PENDING
            );
            return order;
        }

        public void Dispose()
        {
            this._orderEventPublisher.OrderCreated -= this._orderMessageQueue.OnOrderCreated;
        }
    }
}