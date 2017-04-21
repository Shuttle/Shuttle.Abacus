using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface IChangeCalculationOrderCommand
    {
        Guid MethodId { get; set; }
        Guid OwnerId { get; set; }
        List<Guid> OrderedIds { get; }
    }
}
