using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface ISetCalculationConstraintsCommand
    {
        Guid CalculationId { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
