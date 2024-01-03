using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fdcommon.Domain.ValueTypes
{
    public struct OrderId : IBaseId<Guid>
    {
        public OrderId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}