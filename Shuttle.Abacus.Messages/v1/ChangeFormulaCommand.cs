using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ChangeFormulaCommand
    {
        public Guid CalculationId { get; set; }
        public Guid FormulaId { get; set; }
        public List<FormulaOperation> Operations { get; set; }
        public List<OwnedConstraint> Constraints { get; set; }
    }
}