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
                DOrder order = CreateOrderFromDto(customerId, restarentId, orderItems);

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

        private static OrderDto CreateOrderDtoFromOrder(DOrder order)
        {
            return new OrderDto(
                                order.Id.Id,
                                order.CustomerId.Id,
                                order.RestarentId.Id,
                                order.OrderItems.Select((DOrderItem orderItem) =>
                                {
                                    return new OrderItemDto(
                                        orderItem.Id.Id,
                                        orderItem.OrderId.Id,
                                        orderItem.Dishes.Select((DDish dish) =>
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

        private static DOrder CreateOrderFromDto(Guid customerId, Guid restarentId, List<OrderItemDto> orderItems)
        {
            OrderId orderId = new OrderId(Guid.NewGuid());
            List<DOrderItem> newOrderItems = orderItems.Select((OrderItemDto orderItem) =>
            {
                OrderItemId newOrderItemId = new OrderItemId(Guid.NewGuid());

                List<DDish> newDishes = orderItem.Dishes.Select((DishDto dish) =>
                {
                    return new DDish(
                        new DishId(Guid.NewGuid()),
                        newOrderItemId,
                        dish.Name,
                        dish.Quantity,
                        new Price(decimal.Parse(dish.Price.ToString()))
                    );
                }).ToList<DDish>();

                return new DOrderItem(
                    newOrderItemId,
                    orderId,
                    newDishes
                );
            }).ToList<DOrderItem>();

            DOrder order = new DOrder(
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

        public async Task<OrderDto> GetOrder(Guid orderId)
        {
            try
            {
                DOrder order = await this._orderRepository.GetOrder(new OrderId(orderId));
                OrderDto orderDto = CreateOrderDtoFromOrder(order);
                return orderDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}