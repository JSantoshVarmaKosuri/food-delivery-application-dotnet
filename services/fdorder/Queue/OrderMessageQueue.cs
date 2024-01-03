using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdorder.Domain.Entities;

namespace fdorder.Queue
{
    public class OrderMessageQueue
    {
        public OrderMessageQueue()
        {

        }

        public void OnOrderCreated(object sender, Order order)
        {
            Console.WriteLine($"Order {order.Id.Id} create. send message to the queue...");
        }
    }
}