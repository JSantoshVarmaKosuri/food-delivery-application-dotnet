using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fdcommon.Domain.ValueTypes
{
    public struct Price
    {
        public Price(decimal _amount)
        {
            amount = _amount;
        }

        public void addMount(decimal amount)
        {
            this.amount += amount;
        }

        public void substractAMount(decimal amount)
        {
            if (this.amount > amount)
            {
                this.amount -= amount;
            }
            else
            {
                throw new Exception("Amount cannot be less than zero.");
            }
        }

        public decimal getAmountPerQuantity(int quantity)
        {
            return this.amount * quantity;
        }

        private decimal amount { get; set; }
        public decimal Amount { get => this.amount; }
        public static decimal Zero { get; } = 0.00m;
    }
}