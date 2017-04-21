using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IChangeCalculationOrderCommand
    {
        Guid MethodId { get; set; }
        Guid OwnerId { get; set; }
        List<Guid> OrderedIds { get; }
    }
}
