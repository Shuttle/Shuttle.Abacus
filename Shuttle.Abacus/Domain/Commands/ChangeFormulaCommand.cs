using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class ChangeFormulaCommand
    {
        public Guid CalculationId { get; set; }
        public Guid FormulaId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
