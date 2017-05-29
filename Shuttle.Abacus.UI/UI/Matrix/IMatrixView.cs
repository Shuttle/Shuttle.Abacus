using System.Collections.Generic;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public interface IMatrixView : IView
    {
        string DecimalTableNameValue { get; set; }
        IRuleCollection<object> DecimalTableNameRules { set; }
        IRuleCollection<object> RowArgumentRules { set; }
        ArgumentModel RowArgumentModel { get; }
        bool GridInitialized { get; }
        string RowArgumentValue { get; set; }
        string ColumnArgumentValue { get; set; }
        bool HasColumnArgument { get; }
        ArgumentModel ColumnArgumentModel { get; }
        void PopulateArguments(IEnumerable<ArgumentModel> rows);
        void EnableColumnArgument();
        void InitializeGrid();
        void RowValuesOnly();
        void ApplyColumnArgument();
        bool HasInvalidDecimalTable();
        List<MatrixElement> DecimalValues();
        void AddElement(int column, int row, decimal value);
    }
}
