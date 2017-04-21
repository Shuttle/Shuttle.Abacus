using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IChangeCalculationCommand
    {
        Guid CalculationId { get; set; }
        Guid OwnerId { get; set; }
        string OwnerName { get; set; }
        string Name { get; set; }
        bool Required { get; set; }
        List<GraphNodeArgumentDTO> GraphNodeArguments { get; set; }
    }
}
