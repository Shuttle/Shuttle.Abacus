using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class SetCalculationConstraintsCommand 
    {
        public Guid CalculationId { get; set; }
        public List<OwnedConstraint> Constraints { get; set; }
    }
}
