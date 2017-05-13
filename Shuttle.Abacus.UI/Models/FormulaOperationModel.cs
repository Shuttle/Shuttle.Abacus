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

            OperationType = FormulaColumns.OperationColumns.Operation.MapFrom(row);
            ValueSourceType = FormulaColumns.OperationColumns.ValueSource.MapFrom(row);
            ValueSelection = FormulaColumns.OperationColumns.ValueSelection.MapFrom(row);
            Text= FormulaColumns.OperationColumns.Text.MapFrom(row);
        }

        public string OperationType { get; set; }
        public string ValueSourceType { get; set; }
        public string ValueSelection { get; set; }
        public string Text { get; set; }
    }
}