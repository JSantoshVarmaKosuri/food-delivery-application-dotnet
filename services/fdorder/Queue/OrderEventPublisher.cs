using fdorder.Domain.Entities;

namespace fdorder.Queue
{
    public class OrderEventPublisher
    {
        public event EventHandler<DOrder>? OrderCreated;

        protected virtual void OnOrderCreate(DOrder order)
        {
            OrderCreated?.Invoke(this, order);
        }

        public void PublishOnCreatedEvent(DOrder order)
        {
            OnOrderCreate(order);
        }
    }
}