using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaConstraintModel
    {
        public IEnumerable<DataRow> ConstraintTypeRows { get; set; }
        public IEnumerable<DataRow> ArgumentRows { get; set; }
        public IEnumerable<DataRow> ConstraintRows { get; set; }
    }
}