using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaOperationModel
    {
        public FormulaOperationModel()
        {
        }

        public FormulaOperationModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            SequenceNumber = FormulaColumns.OperationColumns.SequenceNumber.MapFrom(row);
            Operation = FormulaColumns.OperationColumns.Operation.MapFrom(row);
            ValueSource = FormulaColumns.OperationColumns.ValueSource.MapFrom(row);
            ValueSelection = FormulaColumns.OperationColumns.ValueSelection.MapFrom(row);
        }

        public int SequenceNumber { get; set; }
        public string Operation { get; set; }
        public string ValueSource { get; set; }
        public string ValueSelection { get; set; }
    }
}