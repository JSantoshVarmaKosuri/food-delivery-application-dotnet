using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdorder.Domain.Entities;

namespace fdorder.Queue
{
    public class OrderEventPublisher
    {
        public event EventHandler<Order>? OrderCreated;

        protected virtual void OnOrderCreate(Order order)
        {
            OrderCreated?.Invoke(this, order);
        }

        public void PublishOnCreatedEvent(Order order)
        {
            OnOrderCreate(order);
        }
    }
}