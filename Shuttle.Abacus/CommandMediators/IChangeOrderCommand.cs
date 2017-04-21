using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface IChangeFormulaOrderCommand
    {
        Guid OwnerId { get; set; }
        string OwnerName { get; set; }
        List<Guid> OrderedIds { get; }
    }
}
