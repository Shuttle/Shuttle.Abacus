using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Models
{
    public class ManageFormulaConstraintsModel
    {
        public List<ArgumentModel> Arguments { get; set; }
        public List<FormulaConstraintModel> Constraints { get; set; }
    }
}