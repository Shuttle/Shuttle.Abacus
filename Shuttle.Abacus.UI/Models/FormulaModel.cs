using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaModel
    {
        public IEnumerable<DecimalTableDTO> DecimalTables { get; set; }
        public IEnumerable<CalculationDTO> PrecedingCalculations { get; set; }
        public IEnumerable<MethodDTO> Methods { get; set; }
        public IEnumerable<ArgumentDTO> Arguments { get; set; }
        public IEnumerable<OperationDTO> FormulaOperations { get; set; }
        public IEnumerable<OperationTypeDTO> OperationTypes { get; set; }
        public IEnumerable<ValueSourceTypeDTO> ValueSourceTypes { get; set; }
    }
}
