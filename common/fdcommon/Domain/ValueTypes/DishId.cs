using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fdcommon.Domain.ValueTypes
{
    public struct DishId : IBaseId<Guid>
    {
        public DishId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}