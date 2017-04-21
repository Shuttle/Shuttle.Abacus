using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class SetCalculationConstraintsCommand : ISetCalculationConstraintsCommand
    {
        public Guid CalculationId { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
