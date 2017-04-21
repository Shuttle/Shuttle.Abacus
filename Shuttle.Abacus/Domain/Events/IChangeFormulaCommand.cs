using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IChangeFormulaCommand
    {
        Guid CalculationId { get; set; }
        Guid FormulaId { get; set; }
        List<OperationDTO> Operations { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
