using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface IChangeFormulaCommand
    {
        Guid CalculationId { get; set; }
        Guid FormulaId { get; set; }
        List<OperationDTO> Operations { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
