using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fdcommon.Domain.ValueTypes;

namespace fdorder.Domain.ValueTypes
{
    public struct OrderItemId : IBaseId<Guid>
    {
        public OrderItemId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}