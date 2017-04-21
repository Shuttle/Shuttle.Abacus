using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class ChangeFormulaCommand : IChangeFormulaCommand
    {
        public Guid CalculationId { get; set; }
        public Guid FormulaId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
