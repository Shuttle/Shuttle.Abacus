using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ISetCalculationConstraintsCommand
    {
        Guid CalculationId { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
