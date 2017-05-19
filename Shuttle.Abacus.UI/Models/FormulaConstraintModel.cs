using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class FormulaConstraintModel
    {
        public FormulaConstraintModel()
        {
        }

        public FormulaConstraintModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            SequenceNumber = FormulaColumns.ConstraintColumns.SequenceNumber.MapFrom(row);
            ArgumentName = FormulaColumns.ConstraintColumns.ArgumentName.MapFrom(row);
            Comparison = FormulaColumns.ConstraintColumns.Comparison.MapFrom(row);
            Value = FormulaColumns.ConstraintColumns.Value.MapFrom(row);
        }

        public string Value { get; set; }
        public string Comparison { get; set; }
        public string ArgumentName { get; set; }
        public int SequenceNumber { get; set; }
    }
}