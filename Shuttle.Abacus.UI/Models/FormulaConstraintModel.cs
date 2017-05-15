using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
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
            ComparisonType = FormulaColumns.ConstraintColumns.ComparisonType.MapFrom(row);
            Value = FormulaColumns.ConstraintColumns.Value.MapFrom(row);
        }

        public string Value { get; set; }
        public string ComparisonType { get; set; }
        public string ArgumentName { get; set; }
        public int SequenceNumber { get; set; }
    }
}