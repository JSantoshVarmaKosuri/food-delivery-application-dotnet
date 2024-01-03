using fdcommon.Domain.Dtos.Order;

namespace fdorder.Domain.Ports
{
    public interface IOrderDomainService
    {
        Task<OrderDto> CreateOrder(
            Guid customerId,
            Guid restarentId,
            List<OrderItemDto> orderItems
        );
        Task<OrderDto> PayOrder(Guid paymentId);
        Task<OrderDto> RejectOrder(Guid orderId);
        Task<OrderDto> ApproveOrder(Guid orderId);
        Task<OrderDto> CancelOrder(Guid orderId);
    }
}