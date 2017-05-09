using System.Collections.Generic;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaOperationPresenterModel
    {
        public IEnumerable<string> OperationTypes { get; set; }
        public IEnumerable<ValueSourceTypeModel> ValueSourceTypes { get; set; }
        public IEnumerable<FormulaOperationModel> FormulaOperations { get; set; }
        public IEnumerable<ArgumentModel> Arguments { get; set; }
    }
}